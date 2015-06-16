Qoollo Logger
======

Powerful logger for .NET applications. 

Main features:
- support most commonly used logging targets out of the box;
- logging targets can be used in any combination;
- support reliable and async logging;
- has configurable log message pattern;
- loggers can be stacked (logger inside library wraps main application logger);
- XSD schema for logger section in App.config.


Supported logging targets:
* Console
* File
* MS SQL Server database
* Pipe service
* Network service
* Logstash


Supported service facilities:
* Group wrapper (enable grouping of any logging targets)
* Async wrapper (enable writing logs in separate thread)
* Reliable wrapper (logs stored in local file while logging target not works)
* Pattern matching wrapper (simple log message routing between logging targets)



## Quick start guide

- Add reference to logger library
- Add configuration section to your 'App.config':
```XML
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="LoggerConfigurationSection" type="Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass, Qoollo.Logger" allowExeDefinition="MachineToApplication" requirePermission="false" />
  </configSections>
  
  <LoggerConfigurationSection xmlns="Qoollo.Logger.Configuration.LoggerConfigurationSection_NS">
    <logger logLevel="DEBUG">
      <asyncQueueWrapper>
        <groupWrapper>
          <consoleWriter />
          <fileWriter fileNameTemplate="logs/{DateTime, format = yyyy-MM-dd}.log" />
          <fileWriter logLevel="ERROR" fileNameTemplate="logs/Errors_{DateTime, format = yyyy-MM-dd}.log" />
        </groupWrapper>
      </asyncQueueWrapper>
    </logger>
  </LoggerConfigurationSection>

</configuration>
```
- Now it is ready to use:
```C#
  Qoollo.Logger.LoggerDefault.Instance.Info("Hello world!");
```


## Extended materials

- [Configuration](https://github.com/qoollo/logger/wiki/Configuration)
- [Usage](https://github.com/qoollo/logger/wiki/Usage)


## NuGet

- [Qoollo.Logger](https://www.nuget.org/packages/Qoollo.Logger)
- [Qoollo.Logger.Config](https://www.nuget.org/packages/Qoollo.Logger.Config)
