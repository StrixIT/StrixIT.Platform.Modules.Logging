/**
 * Copyright 2015 StrixIT
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
CREATE TABLE [dbo].[AnalyticsLog] (
    [Id] [bigint] NOT NULL IDENTITY,
    [ApplicationId] [uniqueidentifier] NOT NULL,
    [GroupId] [uniqueidentifier] NOT NULL,
    [UserId] [uniqueidentifier],
    [UserName] [nvarchar](250),
    [LogType] [nvarchar](50) NOT NULL,
    [LogDateTime] [datetime] NOT NULL,
    [LogData] [nvarchar](4000) NOT NULL,
    CONSTRAINT [PK_dbo.AnalyticsLog] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[AuditLog] (
    [Id] [bigint] NOT NULL IDENTITY,
    [ApplicationId] [uniqueidentifier] NOT NULL,
    [GroupId] [uniqueidentifier] NOT NULL,
    [UserId] [uniqueidentifier],
    [UserName] [nvarchar](250),
    [LogType] [nvarchar](50) NOT NULL,
    [LogDateTime] [datetime] NOT NULL,
    [Message] [nvarchar](1000) NOT NULL,
    CONSTRAINT [PK_dbo.AuditLog] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ErrorLog] (
    [Id] [bigint] NOT NULL IDENTITY,
    [ExceptionType] [nvarchar](100),
    [ApplicationId] [uniqueidentifier] NOT NULL,
    [UserEmail] [nvarchar](250),
    [LogDateTime] [datetime] NOT NULL,
    [Level] [nvarchar](10) NOT NULL,
    [Message] [nvarchar](500) NOT NULL,
    [Exception] [nvarchar](4000),
    [IPAddress] [nvarchar](40),
    [Url] [nvarchar](250),
    [UserAgent] [nvarchar](100),
    [Method] [nvarchar](10),
    [ContentType] [nvarchar](50),
    [Headers] [nvarchar](1000),
    [Cookies] [nvarchar](1000),
    CONSTRAINT [PK_dbo.ErrorLog] PRIMARY KEY ([Id])
)