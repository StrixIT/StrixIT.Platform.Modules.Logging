﻿//#region Apache License
/**
 * Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
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
//#endregion

adminModule.addRoute({ module: 'Logging', type: 'ErrorLog', route: 'Logging/ErrorLog', templateUrl: 'Logging/ErrorLog/Index' });
adminModule.addRoute({ module: 'Logging', type: 'ErrorLog', route: 'Logging/ErrorLog/Details/:id', templateUrl: 'Logging/ErrorLog/Details', dependencies: ["Areas/Logging/Scripts/strixit.logcontroller.min.js"] });
adminModule.addRoute({ module: 'Logging', type: 'AuditLog', route: 'Logging/AuditLog', templateUrl: 'Logging/AuditLog/Index' });
adminModule.addRoute({ module: 'Logging', type: 'AuditLog', route: 'Logging/AuditLog/Details/:id', templateUrl: 'Logging/AuditLog/Details', dependencies: ["Areas/Logging/Scripts/strixit.logcontroller.min.js"] });