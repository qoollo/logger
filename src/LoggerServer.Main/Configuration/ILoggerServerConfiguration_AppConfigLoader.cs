using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Configuration;


namespace LoggerServer.Main.Configuration
{

    /* **** Sample section group *****
    
    public class SampleSectionGroup: ConfigurationSectionGroup
    {
    	public LoggerServerConfigurationConfigClass LoggerServerConfigurationSection
    	{
    		get
    		{
    			return this.Sections["LoggerServerConfigurationSection"] as LoggerServerConfigurationConfigClass;
    		}
    	}
    
    	public ILoggerServerConfiguration LoadLoggerServerConfigurationSection()
    	{
    		return this.LoggerServerConfigurationSection.ExtractConfigData();
    	}
    }
    
    */
    
    
    
    		
    public interface ILoggerServerConfiguration
    {
        ITcpServerConfig TcpServerConfig { get; }
        IPipeServerConfig PipeServerConfig { get; }
    }

    public interface ITcpServerConfig
    {
        Boolean IsEnabled { get; }
        Int32 Port { get; }
        String ServiceName { get; }
    }

    public interface IPipeServerConfig
    {
        Boolean IsEnabled { get; }
        String PipeName { get; }
        String ServiceName { get; }
    }


    // ============================

    public class LoggerServerConfigurationImplement : ILoggerServerConfiguration
    {
        public LoggerServerConfigurationImplement()
        {
            this._tcpServerConfig = new TcpServerConfigImplement();
            this._pipeServerConfig = new PipeServerConfigImplement();
        }

        private ITcpServerConfig _tcpServerConfig;
        public ITcpServerConfig GetTcpServerConfigVal()
        {
            return _tcpServerConfig;
        }
        public void SetTcpServerConfigVal(ITcpServerConfig value)
        {
            _tcpServerConfig = value;
        }
        ITcpServerConfig ILoggerServerConfiguration.TcpServerConfig
        {
            get { return _tcpServerConfig; }
        }

        private IPipeServerConfig _pipeServerConfig;
        public IPipeServerConfig GetPipeServerConfigVal()
        {
            return _pipeServerConfig;
        }
        public void SetPipeServerConfigVal(IPipeServerConfig value)
        {
            _pipeServerConfig = value;
        }
        IPipeServerConfig ILoggerServerConfiguration.PipeServerConfig
        {
            get { return _pipeServerConfig; }
        }


        public LoggerServerConfigurationImplement Copy()
        {
        	var res = new LoggerServerConfigurationImplement();
        
            res._tcpServerConfig = TcpServerConfigImplement.CopyInh(this._tcpServerConfig);
            res._pipeServerConfig = PipeServerConfigImplement.CopyInh(this._pipeServerConfig);

            return res;
        }

        public static ILoggerServerConfiguration CopyInh(ILoggerServerConfiguration src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(LoggerServerConfigurationImplement))
        		return ((LoggerServerConfigurationImplement)src).Copy();
        	if (src.GetType() == typeof(LoggerServerConfigurationImplement))
        		return ((LoggerServerConfigurationImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    public class TcpServerConfigImplement : ITcpServerConfig
    {
        public TcpServerConfigImplement()
        {
            this._isEnabled = true;
            this._serviceName = "LoggerService";
        }

        private Boolean _isEnabled;
        public Boolean GetIsEnabledVal()
        {
            return _isEnabled;
        }
        public void SetIsEnabledVal(Boolean value)
        {
            _isEnabled = value;
        }
        Boolean ITcpServerConfig.IsEnabled
        {
            get { return _isEnabled; }
        }

        private Int32 _port;
        public Int32 GetPortVal()
        {
            return _port;
        }
        public void SetPortVal(Int32 value)
        {
            _port = value;
        }
        Int32 ITcpServerConfig.Port
        {
            get { return _port; }
        }

        private String _serviceName;
        public String GetServiceNameVal()
        {
            return _serviceName;
        }
        public void SetServiceNameVal(String value)
        {
            _serviceName = value;
        }
        String ITcpServerConfig.ServiceName
        {
            get { return _serviceName; }
        }


        public TcpServerConfigImplement Copy()
        {
        	var res = new TcpServerConfigImplement();
        
            res._isEnabled = this._isEnabled;
            res._port = this._port;
            res._serviceName = this._serviceName;

            return res;
        }

        public static ITcpServerConfig CopyInh(ITcpServerConfig src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(TcpServerConfigImplement))
        		return ((TcpServerConfigImplement)src).Copy();
        	if (src.GetType() == typeof(TcpServerConfigImplement))
        		return ((TcpServerConfigImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }

    public class PipeServerConfigImplement : IPipeServerConfig
    {
        public PipeServerConfigImplement()
        {
            this._isEnabled = true;
            this._serviceName = "LoggerService";
        }

        private Boolean _isEnabled;
        public Boolean GetIsEnabledVal()
        {
            return _isEnabled;
        }
        public void SetIsEnabledVal(Boolean value)
        {
            _isEnabled = value;
        }
        Boolean IPipeServerConfig.IsEnabled
        {
            get { return _isEnabled; }
        }

        private String _pipeName;
        public String GetPipeNameVal()
        {
            return _pipeName;
        }
        public void SetPipeNameVal(String value)
        {
            _pipeName = value;
        }
        String IPipeServerConfig.PipeName
        {
            get { return _pipeName; }
        }

        private String _serviceName;
        public String GetServiceNameVal()
        {
            return _serviceName;
        }
        public void SetServiceNameVal(String value)
        {
            _serviceName = value;
        }
        String IPipeServerConfig.ServiceName
        {
            get { return _serviceName; }
        }


        public PipeServerConfigImplement Copy()
        {
        	var res = new PipeServerConfigImplement();
        
            res._isEnabled = this._isEnabled;
            res._pipeName = this._pipeName;
            res._serviceName = this._serviceName;

            return res;
        }

        public static IPipeServerConfig CopyInh(IPipeServerConfig src)
        {
        	if (src == null)
        		return null;
        
        	if (src.GetType() == typeof(PipeServerConfigImplement))
        		return ((PipeServerConfigImplement)src).Copy();
        	if (src.GetType() == typeof(PipeServerConfigImplement))
        		return ((PipeServerConfigImplement)src).Copy();
        
        	throw new Exception("Unknown type: " + src.GetType().ToString());
        }
    }


    // ============================

    public class LoggerServerConfigurationConfigClass : System.Configuration.ConfigurationSection
    {
    	private ILoggerServerConfiguration _configData = new LoggerServerConfigurationImplement();
    	public ILoggerServerConfiguration ConfigData { get {return _configData; } }
     
    
    	public ILoggerServerConfiguration ExtractConfigData()
    	{
    		return LoggerServerConfigurationImplement.CopyInh(_configData);
    	}
    
    	protected override void InitializeDefault()
        {
    		base.InitializeDefault();
            _configData = new LoggerServerConfigurationImplement();
        }
    
        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
    		if (reader.NodeType == System.Xml.XmlNodeType.None)
    			reader.Read();
    		_configData = DeserializeILoggerServerConfigurationElem(reader);
        }
    
        public override bool IsReadOnly()
        {
    		return true;
        }
    	
    
    	private T Parse<T>(string value)
        {
    		return (T)System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value);
        }
    
    
        private List<T> DeserializeList<T>(System.Xml.XmlReader reader, Func<System.Xml.XmlReader, T> readFnc, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            List<T> res = new List<T>();
        
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                return res;
            }
        
            string initialName = reader.Name;
        
            reader.ReadStartElement();
        
            do
            {
                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    if (expectedName != null && reader.Name != expectedName)
                        throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
                    T elem = readFnc(reader);
                    res.Add(elem);
                }
                else
                {
                    reader.Skip();
                }
            }
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
        
        	reader.ReadEndElement();
        
            return res;
        }
    
    
        private Dictionary<TKey, TValue> DeserializeDictionary<TKey, TValue>(System.Xml.XmlReader reader, Func<System.Xml.XmlReader, TValue> readFnc, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            Dictionary<TKey, TValue> res = new Dictionary<TKey, TValue>();
        
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                return res;
            }
        
            string initialName = reader.Name;
        
            reader.ReadStartElement();
        
            do
            {
                if (reader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    if (expectedName != null && reader.Name != expectedName)
                        throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
                    string strKey = reader.GetAttribute("key");
                    if (strKey == null)
                        throw new System.Configuration.ConfigurationErrorsException("Key not found for dictionary: " + reader.Name, reader);
        
                    TKey key = Parse<TKey>(strKey);
                    TValue val = readFnc(reader);
        
                    res.Add(key, val);
                }
                else
                {
                    reader.Skip();
                }
            }
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
        
        	reader.ReadEndElement();
        
            return res;
        }
        
        
        private T DeserializeSimpleValueElement<T>(System.Xml.XmlReader reader)
        {
    		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            string ElemName = reader.Name;
        
            string addValue = reader.GetAttribute("value");
            if (addValue == null)
                throw new System.Configuration.ConfigurationErrorsException("Value not found for SimpleValueElement '" + ElemName + "' inside element", reader);
        
            T res = Parse<T>(addValue);
        
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Read();
                if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                    throw new System.Configuration.ConfigurationErrorsException("SimpleValueElement '" + ElemName + "' can't contains any other elements", reader);
                reader.ReadEndElement();
            }
        
            return res;
        }
    
    
        private T DeserializeSimpleValueElement<T>(System.Xml.XmlReader reader, string expectedName)
        {
    	    if (reader.NodeType != System.Xml.XmlNodeType.Element)
                throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
    
            if (expectedName != null && reader.Name != expectedName)
                throw new System.Configuration.ConfigurationErrorsException("Unexpected element name inside list: " + reader.Name, reader);
    
            string ElemName = reader.Name;
    
            string addValue = reader.GetAttribute("value");
            if (addValue == null)
                throw new System.Configuration.ConfigurationErrorsException("Value not found for SimpleValueElement '" + ElemName + "' inside element", reader);
    
            T res = Parse<T>(addValue);
    
            if (reader.IsEmptyElement)
            {
                reader.Read();
            }
            else
            {
                reader.Read();
                if (reader.MoveToContent() != System.Xml.XmlNodeType.EndElement)
                    throw new System.Configuration.ConfigurationErrorsException("SimpleValueElement '" + ElemName + "' can't contains any other elements", reader);
                reader.ReadEndElement();
            }
    
            return res;
        }
    
    
    
        private ILoggerServerConfiguration DeserializeILoggerServerConfigurationElem(System.Xml.XmlReader reader)
        {
        	var res = new LoggerServerConfigurationImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        

            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'ILoggerServerConfiguration'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'ILoggerServerConfiguration'", reader);
                                }
                            case "tcpServerConfig":
                                res.SetTcpServerConfigVal(DeserializeITcpServerConfigElem(reader));
                                parsedElements.Add("tcpServerConfig");
                                break;
                            case "pipeServerConfig":
                                res.SetPipeServerConfigVal(DeserializeIPipeServerConfigElem(reader));
                                parsedElements.Add("pipeServerConfig");
                                break;
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'ILoggerServerConfiguration': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.Add("tcpServerConfig");
            restElems.Add("pipeServerConfig");
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private ILoggerServerConfiguration DeserializeILoggerServerConfigurationElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'ILoggerServerConfiguration': " + reader.Name, reader);
            
        	return DeserializeILoggerServerConfigurationElem(reader);
        }

        private ILoggerServerConfiguration DeserializeILoggerServerConfigurationElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "loggerServerConfiguration":
                    return DeserializeILoggerServerConfigurationElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'ILoggerServerConfiguration'", reader);
            }
        }


        private ITcpServerConfig DeserializeITcpServerConfigElem(System.Xml.XmlReader reader)
        {
        	var res = new TcpServerConfigImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("isEnabled");
            if (attribGenTempVal != null)
                res.SetIsEnabledVal(Parse<Boolean>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("port");
            if (attribGenTempVal != null)
                res.SetPortVal(Parse<Int32>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'port for element 'ITcpServerConfig' not defined", reader);

            attribGenTempVal = reader.GetAttribute("serviceName");
            if (attribGenTempVal != null)
                res.SetServiceNameVal(Parse<String>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'ITcpServerConfig'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'ITcpServerConfig'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'ITcpServerConfig': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private ITcpServerConfig DeserializeITcpServerConfigElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'ITcpServerConfig': " + reader.Name, reader);
            
        	return DeserializeITcpServerConfigElem(reader);
        }

        private ITcpServerConfig DeserializeITcpServerConfigElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "tcpServerConfig":
                    return DeserializeITcpServerConfigElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'ITcpServerConfig'", reader);
            }
        }


        private IPipeServerConfig DeserializeIPipeServerConfigElem(System.Xml.XmlReader reader)
        {
        	var res = new PipeServerConfigImplement();
        
        	HashSet<string> parsedElements = new HashSet<string>();
        
            string attribGenTempVal = null;
            attribGenTempVal = reader.GetAttribute("isEnabled");
            if (attribGenTempVal != null)
                res.SetIsEnabledVal(Parse<Boolean>(attribGenTempVal));

            attribGenTempVal = reader.GetAttribute("pipeName");
            if (attribGenTempVal != null)
                res.SetPipeNameVal(Parse<String>(attribGenTempVal));
            else
                throw new System.Configuration.ConfigurationErrorsException("Attribute 'pipeName for element 'IPipeServerConfig' not defined", reader);

            attribGenTempVal = reader.GetAttribute("serviceName");
            if (attribGenTempVal != null)
                res.SetServiceNameVal(Parse<String>(attribGenTempVal));


            if (reader.IsEmptyElement)
            {
            	reader.Skip();
            }
            else
            {
            	string initialName = reader.Name;
                reader.ReadStartElement();
            	do
                {
            		if (reader.NodeType != System.Xml.XmlNodeType.Element)
                    {
            			reader.Skip();
            		}
            		else
            		{			
                        switch (reader.Name)
                        {
                            case "add":
                                string addKey = reader.GetAttribute("key");
                                if (addKey == null)
                                	throw new System.Configuration.ConfigurationErrorsException("Key not found for 'add' inside element 'IPipeServerConfig'", reader);	
                                
                                switch (addKey)
                                {
                                	default:
                                		throw new System.Configuration.ConfigurationErrorsException("Unknown key " + addKey + " inside element 'IPipeServerConfig'", reader);
                                }
                            default:
                                throw new System.Configuration.ConfigurationErrorsException("Unknown element inside 'IPipeServerConfig': " + reader.Name, reader);
                        }
            		}
            	}
            	while (reader.NodeType != System.Xml.XmlNodeType.EndElement || reader.Name != initialName);
            
                reader.ReadEndElement();
            }
            
            HashSet<string> restElems = new HashSet<string>();
            restElems.RemoveWhere(o => parsedElements.Contains(o));
            if (restElems.Count > 0)
                throw new System.Configuration.ConfigurationErrorsException("Not all required properties readed: " + string.Join(", ",restElems));
            return res;
        }

        private IPipeServerConfig DeserializeIPipeServerConfigElem(System.Xml.XmlReader reader, string expectedName)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Expected Element node type", reader);
        
        	if (expectedName != null && reader.Name != expectedName)
        		throw new System.Configuration.ConfigurationErrorsException("Unexpected element name for type 'IPipeServerConfig': " + reader.Name, reader);
            
        	return DeserializeIPipeServerConfigElem(reader);
        }

        private IPipeServerConfig DeserializeIPipeServerConfigElemWithInh(System.Xml.XmlReader reader)
        {
        	if (reader.NodeType != System.Xml.XmlNodeType.Element)
        		throw new System.Configuration.ConfigurationErrorsException("Not an Element node type", reader);
        
        	switch (reader.Name)
        	{
                case "pipeServerConfig":
                    return DeserializeIPipeServerConfigElem(reader);
                default:
                    throw new System.Configuration.ConfigurationErrorsException("Unknown child type name: '" + reader.Name + "' for base type 'IPipeServerConfig'", reader);
            }
        }


    }


}
