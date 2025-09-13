# Welcome to OCCSVG

OCCSVGViewer is a <b>DEMO 3D-Viewer</b> built on <b>Occt.NET (7.9.0)</b>. It uses the <b>OCCSVG NuGet package</b> to read, interpret, and render SVG files. 
The package, available at: <code style="color : red">Missing link to download from NuGet</code>, is designed to import various SVG elements into a .NET application. 
The OCCSVGViewer application also displays the topology of the SVG model and its elements in a TreeView.

The <b>OCCSVG NuGet package</b> is built upon several key technologies: .Net, C#, C++/CLI. It also uses Occt.NET (version 7.9.0), which is based on Open CASCADE Technology (OCCT), is available from: https://www.nuget.org/packages/Occt.NET.

<b>OCCSVG</b> can accurately read shapes defined in SVG files, including their strokes, fills, and other properties. 
It supports most types of gradients, patterns, which gives designers the freedom to define their drawings in almost any vector drawing application and save them into SVG file. 
Application developers can then use the drawings in their .NET applications. 

# About OpenCASCADE Technology (OCCT)
The restore script downloads a pre-built version of Open CASCADE Technology (OCCT) so that the project can be built immediately. 
This package contains only the parts that are used in this project. The complete distribution can be cloned from the
https://github.com/Open-Cascade-SAS/OCCT
This allows to use additional parts, build the library with other build options or to make code changes. The currently used version can be found in the about dialog.
