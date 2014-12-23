<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="d0ed9acb-0435-4532-afdd-b5115bc4d562" namespace="LoggerConfig" xmlSchemaNamespace="LoggerConfig" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
    <enumeratedType name="Level" namespace="LoggerConfig">
      <literals>
        <enumerationLiteral name="TRACE" value="0" />
        <enumerationLiteral name="DEBUG" value="1" />
        <enumerationLiteral name="INFO" value="2" />
        <enumerationLiteral name="WARN" value="3" />
        <enumerationLiteral name="ERROR" value="4" />
        <enumerationLiteral name="FATAL" value="5" />
      </literals>
    </enumeratedType>
    <enumeratedType name="ConfigurationType" namespace="LoggerConfig">
      <literals>
        <enumerationLiteral name="ConsoleLoggerConfiguration" />
        <enumerationLiteral name="FileLoggerConfiguration" />
        <enumerationLiteral name="DBLoggerConfiguration" />
        <enumerationLiteral name="PipeLoggerConfiguration" />
        <enumerationLiteral name="NetLoggerConfiguration" />
        <enumerationLiteral name="RoutingLoggerConfiguration" />
      </literals>
    </enumeratedType>
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="LogProperties" accessModifier="Internal" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="logProperties">
      <attributeProperties>
        <attributeProperty name="IsEnable" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isEnable" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="LoggerConfigurations" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="loggerConfigurations" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurations" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="ConsoleLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="Template" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="template" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="FileLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="Filename" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="filename" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Template" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="template" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsNeedRotate" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="isNeedRotate" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Encoding" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="encoding" isReadOnly="false" defaultValue="&quot;utf-8&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="DBLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="ConnectionString" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="connectionString" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="StoreProcedureName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="storeProcedureName" isReadOnly="false" defaultValue="&quot;LogInsert&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="PipeLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="ServerName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="serverName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="PipeName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="pipeName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="NetLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="ServerName" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="serverName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="RoutingLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <elementProperties>
        <elementProperty name="BindingByModuleName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="bindingByModuleName" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/BindingByModuleName" />
          </type>
        </elementProperty>
        <elementProperty name="FromAll" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fromAll" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FromAll" />
          </type>
        </elementProperty>
        <elementProperty name="FromOthers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="fromOthers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/FromOthers" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="LoggerConfigurations" accessModifier="Internal" xmlItemName="loggerConfigurationBase" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="BindingByModuleName" accessModifier="Internal" xmlItemName="module" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Module" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="Module" accessModifier="Internal" xmlItemName="logger" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="ModuleName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="moduleName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Logger" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="Logger" accessModifier="Internal">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="FromAll" accessModifier="Internal" xmlItemName="logger" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Logger" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="FromOthers" accessModifier="Internal" xmlItemName="logger" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Logger" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="LoggerConfigurationBase" accessModifier="Internal">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Level" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="level" isReadOnly="false">
          <type>
            <enumeratedTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Level" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="GroupLogger" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <elementProperties>
        <elementProperty name="Loggers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="loggers" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Loggers" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElementCollection name="Loggers" accessModifier="Internal" xmlItemName="logger" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Logger" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="AsyncQueue" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/LoggerConfigurationBase" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="QueueSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="queueSize" isReadOnly="false" defaultValue="200">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsDiscardExcess" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isDiscardExcess" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsRepeatLoggingIfErrors" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isRepeatLoggingIfErrors" isReadOnly="false" defaultValue="true">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Logger" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logger" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Logger" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationElement>
    <configurationElement name="AsyncQueueWithReliableSending" accessModifier="Internal">
      <baseClass>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/AsyncQueue" />
      </baseClass>
      <attributeProperties>
        <attributeProperty name="FolderNameOfTemporaryStore" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="folderNameOfTemporaryStore" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxFileSize" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxFileSize" isReadOnly="false" defaultValue="10485760L">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int64" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>