# Welcome to OCCSVG

OCCSVGViewer is a <b>DEMO 3D-Viewer</b> built on <b>Occt.NET (7.9.0)</b>. It uses the <b>OCCSVG NuGet package</b> to read, interpret, and render SVG files. 
The package, available at: [OCCSVG NuGet Package](https://www.nuget.org/packages/OCCSVG), is designed to import various SVG elements into a .NET application. 
The OCCSVGViewer application also displays the topology of the SVG model and its elements in a TreeView.

The <b>OCCSVG NuGet package</b> is built upon several key technologies: .Net, C#, C++/CLI. It also uses Occt.NET (version 7.9.0), which is based on Open CASCADE Technology (OCCT), is available from: 
[Occt.NET NuGet Package](https://www.nuget.org/packages/Occt.NET).

<b>OCCSVG</b> can accurately read shapes defined in SVG files, including their strokes, fills, and other properties. 
It supports most types of gradients, patterns, which gives designers the freedom to define their drawings in almost any vector drawing application and save them into SVG file. 
Application developers can then use the drawings in their .NET applications. <br>

<img width="939" height="571" alt="image" src="https://github.com/user-attachments/assets/8c336e3e-ef01-4456-81c0-9a07036cb918" />
<img width="940" height="576" alt="image" src="https://github.com/user-attachments/assets/26e2ee65-46e3-4d0f-994f-285ff3892cf8" />

<br>

## Development requirements 
* Microsoft Visual Studio Professional 2022 (64-bit)
  * .Net Desktop workload</li>
  * .Net 8 support component</li>
  * C++/CLI support component </li>

<br>

## Unsupported Features (for OCCSVG Beta Version)
* Reading <b>marker</b> elements (e.g., line starting and ending shapes)
* Reading <b>clipPath</b> and <b>mask</b>
* Reading <b>image</b> currently not supported
* Reading <b>text</b> on path

<br>

## About Open CASCADE Technology (OCCT)
The complete distribution can be cloned from the https://github.com/Open-Cascade-SAS/OCCT.
This allows you to use additional parts, build the library with different options or make code changes. The currently used version can be found in the about dialog.

<br>

## Software Description

<b>Start Application</b><br>
Before you start the application, make sure that the Occt.NET dll is copied into the Application’s debug or release folder.<br>

From: OCCSVGViewer\bin\Debug\net8.0-windows\occt\x64\
<br>
<img width="526" height="236" alt="image" src="https://github.com/user-attachments/assets/53b8efb8-0bd1-4f72-9828-07cb6a4a26c6" />
<br>
To: OCCSVGViewer\bin\Debug\net8.0-windows\
<br>
<img width="522" height="269" alt="image" src="https://github.com/user-attachments/assets/b3f2fc2a-8df1-4176-b267-abfed4a1863b" />
<br>

<b>Unit pixel</b><br>
The default unit is currently pixels. Later, user will be able to choose their own units, such as inches, points, centimeters or millimeters.<br>

<img width="555" height="156" alt="image" src="https://github.com/user-attachments/assets/497a055b-8417-4189-892d-31a149e5fad2" />
<br>

<br>
<b>Display mode (Wire or Shaded)</b><br>
<b>Wire:</b> The SVG elements will be displayed in Wireframe and it's no longer possible to show them in shaded mode. Here you can handle the element as a Curves model, which reduces the computational effort to display them.
<br>
<b>Shaded:</b> In "Shaded mode", the elements retain their original background color (stored in the SVG file). You can handle the element here as Face and Curves model.



