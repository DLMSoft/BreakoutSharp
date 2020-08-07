BreakoutSharp
====================
Resources and idea from [LearnOpenGL](http://learnopengl.com/) tutorial [example game](https://github.com/JoeyDeVries/LearnOpenGL/tree/master/src/7.in_practice/3.2d_game/0.full_source) by [JoeyDeVries](http://joeydevries.com/), based on the [Creative Commons BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/) license, this project can not used for comercial usage.

The code of this project is C# language and uses OpenTK for OpenGL and OpenAL wrapper.

All code part of this project is pulish under LGPL v2.1.

Copyright
----------------
Copyright (C) 2020 DLM Soft / Tamashii

This project is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This project is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
USA

Build
---------------
The environments of this project is .Net Core 3.1 for Windows Desktop, so you need to build it in **Windows** platform and make sure installed [.Net Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.302-windows-x64-installer).

Once you installed .Net core SDK, open cmd or other terminal, go to project directory, and input :
```sh
dotnet restore
```
Then wait for a while, your project will fully functional.

To run the project, input:
```sh
dotnet run
```

To build the project for release :
- x86 :
    ```sh
    dotnet build -c Release -r win-x86 /p:PublishSingleFile=true
    ```
- x64 :
    ```sh
    dotnet build -c Release -r win-x64 /p:PublishSingleFile=true
    ```