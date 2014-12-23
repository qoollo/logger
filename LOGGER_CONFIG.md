Qoollo Logger configuration
======

There's 2 way to configure logger:
- from App.config,
- from source code.

## App.config

To configure logger from App.config you should place the logger configuration section with name 'LoggerConfigurationSection'.
This section can also be placed inside any section group.
To enable IntelliSense you can add XSD schema to you project (as file or from NuGet packet).

Sample App.config:

```XML
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="LoggerConfigurationSection" type="Qoollo.Logger.Configuration.LoggerConfigurationSectionConfigClass, Qoollo.Logger" allowExeDefinition="MachineToApplication" requirePermission="false" />
  </configSections>
  
  <LoggerConfigurationSection xmlns="Qoollo.Logger.Configuration.LoggerConfigurationSection_NS">
    <logger logLevel="TRACE">
      <asyncQueueWrapper maxQueueSize="4096">
        <groupWrapper>
          <emptyWriter />
          
          <consoleWriter logLevel="INFO"
            template="{DateTime}. {Level}. \n At {StackSource}.{Class}::{Method}.\n Message: {Message}. {Exception, prefix = '\n Exception: ', valueOnNull=''}\n\n"/>
          
          <fileWriter logLevel="DEBUG" fileNameTemplate="logs/{DateTime, format = yyyy-MM-dd}.log"
            template="{DateTime}. {Level}. \n At {StackSource}.{Class}::{Method}.\n Message: {Message}. {Exception, prefix = '\n Exception: ', valueOnNull=''}\n\n"/>
          
          <databaseWriter logLevel="INFO" connectionString="Data Source = (local); Database = LogsDb; Integrated Security=SSPI;" storedProcedureName="[dbo].[LogInsert]" />
          
          <asyncReliableQueueWrapper folderForTemporaryStore="net_rel">
            <networkWriter logLevel="DEBUG" serverAddress="127.0.0.1" />
          </asyncReliableQueueWrapper>
          
          <asyncReliableQueueWrapper folderForTemporaryStore="pipe_rel">
            <pipeWriter logLevel="DEBUG" pipeName="LogPipe" />
          </asyncReliableQueueWrapper>

          <patternMatchingWrapper pattern="{Level}">
            <match value="WARN">
              <fileWriter fileNameTemplate="logs/WarnOnly_{DateTime, format = yyyy-MM-dd}.log"/>
            </match>
            <default>
              <fileWriter fileNameTemplate="logs/PatternDefault_{DateTime, format = yyyy-MM-dd}.log"/>
            </default>
          </patternMatchingWrapper>
          
        </groupWrapper>
      </asyncQueueWrapper>
    </logger>
  </LoggerConfigurationSection>
  
</configuration>
```