using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace Qoollo.Logger.Writers.Wrappers.Helpers.QueueAsyncProcessing
{
    /// <summary>
    /// Асинхронный обработчик данных в несколько потоков с очередью
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемого элемента</typeparam>
    internal abstract class QueueAsyncProcessor<T>: QueueAsyncProcessorBase<T>
    {
        /// <summary>
        /// Контракты
        /// </summary>
        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(_procThreads != null);
            Contract.Invariant(_queue != null);
            Contract.Invariant(_name != null);
            Contract.Invariant(_activeThreadCount >= 0);
            Contract.Invariant(Enum.IsDefined(typeof(QueueAsyncProcessorState), (QueueAsyncProcessorState)_state));
        }

        private CancellationTokenSource _threadWaitCancelation;
        private Thread[] _procThreads;
        private int _activeThreadCount;
        private string _name;
        private bool _isBackground;
        private BlockingCollection<T> _queue;
        private int _maxQueueSize;
        private int _state = 0;
        private volatile bool _lockAdding = false;
        private volatile bool _letFinishProccess = false;
        private volatile bool _isDisposed = false;

        /// <summary>
        /// Конструктор QueueAsyncProcessor
        /// </summary>
        /// <param name="processorCount">Число потоков обработки</param>
        /// <param name="maxQueueSize">Максимальный размер очереди</param>
        /// <param name="name">Имя, присваемое потокам</param>
        /// <param name="isBackground">Будут ли потоки работать в фоновом режиме</param>
        public QueueAsyncProcessor(int processorCount, int maxQueueSize, string name, bool isBackground)
        {
            Contract.Requires(processorCount > 0);

            _isBackground = isBackground;
            _name = name ?? this.GetType().Name;
            _procThreads = new Thread[processorCount];
            _activeThreadCount = 0;

            if (maxQueueSize > 0)
            {
                _maxQueueSize = maxQueueSize;
                _queue = new BlockingCollection<T>(new ConcurrentQueue<T>(), maxQueueSize);
            }
            else
            {
                _maxQueueSize = -1;
                _queue = new BlockingCollection<T>(new ConcurrentQueue<T>());
            }
        }


        /// <summary>
        /// Запущен ли сейчас обработчик
        /// </summary>
        public bool IsWorkState
        {
            get { return State == QueueAsyncProcessorState.InWork; }
        }

        /// <summary>
        /// Имя обработчика
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Работают ли потоки в фоновом режиме
        /// </summary>
        public bool IsBackground
        {
            get { return _isBackground; }
        }

        /// <summary>
        /// Число потоков обработки
        /// </summary>
        public int ProcessorCount
        {
            get { return _procThreads.Length; }
        }


        /// <summary>
        /// Число элементов в очереди
        /// </summary>
        public int ElementCount
        {
            get { return _queue.Count; }
        }


        /// <summary>
        /// Текущее состояние
        /// </summary>
        public QueueAsyncProcessorState State
        {
            get { return (QueueAsyncProcessorState)_state; }
        }


        /// <summary>
        /// Запуск обработчиков.
        /// Возможен перезапуск после вызова метода Stop()
        /// </summary>
        public void Start()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            var prevState = State;
            if (prevState != QueueAsyncProcessorState.Created && prevState != QueueAsyncProcessorState.Stopped)
                throw new InvalidOperationException("Can't start QueueAsyncProcessor cause it is in wrong state: " + prevState.ToString());

            if (Interlocked.CompareExchange(ref _state, (int)QueueAsyncProcessorState.StartPending, (int)prevState) != (int)prevState)
                throw new InvalidOperationException("Can't start QueueAsyncProcessor cause it is in wrong state: " + State.ToString());


            _threadWaitCancelation = new CancellationTokenSource();

            for (int i = 0; i < _procThreads.Length; i++)
            {
                _procThreads[i] = new Thread(new ThreadStart(ThreadProcFunc));
                _procThreads[i].IsBackground = _isBackground;
                if (_name != null)
                    _procThreads[i].Name = string.Format("QueueAsyncProcessor. {0}. Thread #{1}", _name, i.ToString());
                _procThreads[i].Start();
            }

            _lockAdding = false;

            prevState = (QueueAsyncProcessorState)Interlocked.CompareExchange(ref _state, (int)QueueAsyncProcessorState.InWork, (int)QueueAsyncProcessorState.StartPending);
            Contract.Assume(prevState == QueueAsyncProcessorState.StartPending || prevState == QueueAsyncProcessorState.Disposed);

            if (_isDisposed && _threadWaitCancelation != null)
                _threadWaitCancelation.Cancel();

        }

        /// <summary>
        /// Остановка работы асинхронного обработчика
        /// </summary>
        /// <param name="waitForStop">Ждать ли завершения всех потоков</param>
        /// <param name="letFinishProcess">Позволить закончить обработку того, что есть в очереди</param>
        /// <param name="completeAdding">Заблокировать добавление новых элементов</param>
        /// <returns>Запущен ли процесс остановки</returns>
        public bool Stop(bool waitForStop, bool letFinishProcess, bool completeAdding)
        {
            if (State != QueueAsyncProcessorState.InWork)
                return false;

            if (Interlocked.CompareExchange(ref _state, (int)QueueAsyncProcessorState.StopPending, (int)QueueAsyncProcessorState.InWork) != (int)QueueAsyncProcessorState.InWork)
                return false;

            _lockAdding = completeAdding;
            _letFinishProccess = letFinishProcess;

            _threadWaitCancelation.Cancel();

            for (int i = 0; i < _procThreads.Length; i++)
            {
                var prc = _procThreads[i];
                _procThreads[i] = null;
                if (prc != null && waitForStop)
                    prc.Join();
            }

            if (_activeThreadCount == 0 && State == QueueAsyncProcessorState.StopPending)
            {
                var prevState = (QueueAsyncProcessorState)Interlocked.CompareExchange(ref _state, (int)QueueAsyncProcessorState.Stopped, (int)QueueAsyncProcessorState.StopPending);
                Contract.Assume(prevState == QueueAsyncProcessorState.StopPending || prevState == QueueAsyncProcessorState.Disposed || prevState == QueueAsyncProcessorState.Stopped);
            }

            Contract.Assume(!waitForStop || State == QueueAsyncProcessorState.Stopped || State == QueueAsyncProcessorState.Disposed);
            return true;
        }

        /// <summary>
        /// Остановка работы асинхронного обработчика
        /// </summary>
        public void Stop()
        {
            Stop(true, false, false);
        }

        /// <summary>
        /// Ожидание полной остановки
        /// </summary>
        public void WaitUntilStop()
        {
            SpinWait sw = new SpinWait();
            while (State != QueueAsyncProcessorState.Stopped && State != QueueAsyncProcessorState.Disposed)
                sw.SpinOnce();
        }

        /// <summary>
        /// Ожидание полной остановки с таймаутом
        /// </summary>
        /// <param name="timeout">Таймаут ожидания в миллисекундах</param>
        /// <returns>true - дождались, false - вышли по таймауту</returns>
        public bool WaitUntilStop(int timeout)
        {
            return SpinWait.SpinUntil(() => State == QueueAsyncProcessorState.Stopped || State == QueueAsyncProcessorState.Disposed, timeout);
        }


        /// <summary>
        /// Попытаться добавить элемент на обработку
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <returns>Удалось ли добавить</returns>
        public override bool TryAdd(T element)
        {
            if (_lockAdding)
                throw new InvalidOperationException("Adding new elements is locked. QueueAsyncProcessor is going to stop");

            return _queue.TryAdd(element);
        }

        /// <summary>
        /// Добавление элемента на обработку
        /// </summary>
        /// <param name="element">Элемент</param>
        public override void Add(T element)
        {
            if (_lockAdding)
                throw new InvalidOperationException("Adding new elements is locked. QueueAsyncProcessor is going to stop");
            _queue.Add(element);
        }

        /// <summary>
        /// Запрошена ли остановка потоков
        /// </summary>
        protected bool IsStopingRequested
        {
            get { return ExtractToken().IsCancellationRequested; }
        }

        /// <summary>
        /// Извлечение токена отмены. (Обрабатывает ситуацию, когда _threadWaitCancelation == null)
        /// </summary>
        /// <returns>Токен отмены</returns>
        private CancellationToken ExtractToken()
        {
            CancellationTokenSource tokSrc = Volatile.Read(ref _threadWaitCancelation);
            if (tokSrc != null)
                return tokSrc.Token;

            return new CancellationToken(true);
        }

        /// <summary>
        /// Основноя функция, выполняемая потоками
        /// </summary>
        private void ThreadProcFunc()
        {
            CancellationToken token = ExtractToken();
            bool iterateOverException = true;

            object state = null;

            try
            {
                Interlocked.Increment(ref _activeThreadCount);

                state = Prepare();

                while ((!token.IsCancellationRequested || (_letFinishProccess && _queue.Count > 0)) && iterateOverException)
                {
                    try
                    {
                        while (!token.IsCancellationRequested)
                        {
                            T elem = _queue.Take(token);
                            Process(elem, state, token);
                        }

                        if (_letFinishProccess)
                        {
                            T elem = default(T);
                            while (_queue.TryTake(out elem))
                            {
                                Process(elem, state, token);
                            }
                        }
                    }
                    catch (ThreadAbortException)
                    {
                        iterateOverException = false;
                    }
                    catch (ThreadInterruptedException)
                    {
                        iterateOverException = false;
                    }
                    catch (OperationCanceledException opEx)
                    {
                        if (opEx.CancellationToken != token)
                            iterateOverException = ProcessThreadException(opEx);
                    }
                    catch (Exception ex)
                    {
                        iterateOverException = ProcessThreadException(ex);
                    }
                }
            }
            finally
            {
                Finalize(state);

                if (Interlocked.Decrement(ref _activeThreadCount) <= 0)
                {
                    if (State == QueueAsyncProcessorState.StopPending)
                    {
                        Interlocked.CompareExchange(ref _state, (int)QueueAsyncProcessorState.Stopped, (int)QueueAsyncProcessorState.StopPending);
                    }
                }
            }
        }


        /// <summary>
        /// Обработка исключений. 
        /// Чтобы исключение было проброшено наверх, нужно выбросить новое исключение внутри метода.
        /// </summary>
        /// <param name="ex">Исключение</param>
        /// <returns>Игнорировать ли исключение (false - поток завершает работу)</returns>
        protected virtual bool ProcessThreadException(Exception ex)
        {
            Contract.Requires(ex != null);

            throw new QueueAsyncProcessorException("QueueAsyncProcessor processing exception", ex);
        }

        /// <summary>
        /// Создание объекта состояния на поток.
        /// Вызывается при старте для каждого потока
        /// </summary>
        /// <returns>Объект состояния</returns>
        protected virtual object Prepare()
        {
            return null;
        }
        /// <summary>
        /// Основной метод обработки элементов
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="state">Объект состояния, инициализированный в методе Prepare()</param>
        /// <param name="token">Токен для отмены обработки</param>
        protected abstract void Process(T element, object state, CancellationToken token);

        /// <summary>
        /// Освобождение объекта состояния потока
        /// </summary>
        /// <param name="state">Объект состояния</param>
        protected virtual void Finalize(object state)
        {
        }



        /// <summary>
        /// Основной код освобождения ресурсов
        /// </summary>
        /// <param name="isUserCall">Вызвано ли освобождение пользователем. False - деструктор</param>
        protected override void Dispose(bool isUserCall)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;

                if (isUserCall)
                    Stop(true, false, true);

                if (isUserCall && _queue != null)
                    _queue.CompleteAdding();
            }

            var token = Volatile.Read(ref _threadWaitCancelation);
            if (token != null)
                token.Cancel();

            _state = (int)QueueAsyncProcessorState.Disposed;

            base.Dispose(isUserCall);
        }

        /// <summary>
        /// Финализатор
        /// </summary>
        ~QueueAsyncProcessor()
        {
            Dispose(false);
        }
    }
}
