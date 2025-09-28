# Welcome to OCCSVG.NET

## OCCSVG.NET 
This is a library designed for reading SVG files (Scalable Vector Graphic), accurately interpreting shapes, 
including their strokes, fills, and other properties. 
It supports most types of gradients, patterns, which gives designers the freedom to define their drawings in almost 
any vector drawing application and save them into SVG file. 

Additionally, the **OCCSVGViewer** application can view and explore SVG files. 
<br>

## OCC SVG Viewer 
The application is a demo using 3D-Viewer built on **Occt.NET (7.9.0)**. 
The demo is available on the [GitHub](https://github.com/RabihKaddour/OCCSVGViewer), displays also the SVG model's topology and its elements in a TreeView. 
It allows for editing the elements in Tree, so it can be used to see how arbitrary SVG code will be displayed using the library (see picture below). 
<br>

<img width="1262" height="800" alt="screenshot11" src="https://github.com/user-attachments/assets/7959ffaa-e489-4128-b6c9-4701231a7288" />

<br><br>

The **OCCSVG.NET package** is built upon several key technologies: .Net, C#, C++/CLI. It also uses Occt.NET (version 7.9.0), 
which is based on Open CASCADE Technology (OCCT), is available from: https://www.nuget.org/packages/Occt.NET.
<br>


You can also use <b>OCCSVG.NET.dll</b> for testing, copied in the Libraries folder <br>
<img width="299" height="143" alt="image" src="https://github.com/user-attachments/assets/ad176e8d-7632-456f-b440-f63c58445364" />
<br>

<img width="282" height="134" alt="image" src="https://github.com/user-attachments/assets/ecd7627e-8eec-4e00-ac82-c3874a02087b" />
<br><br>

## Software Face
<br><br>
<img width="939" height="571" alt="image" src="https://github.com/user-attachments/assets/8c336e3e-ef01-4456-81c0-9a07036cb918" />

<br>

## Development requirements 
* Microsoft Visual Studio Professional 2022 (64-bit)**
  * .Net Desktop workload
  * .Net 8 support component
  * C++/CLI support component 

<br>

## Unsupported Features (for OCCSVG Beta Version)
* Reading <b>marker</b> elements (e.g., line starting and ending shapes)
* Reading <b>clipPath</b> and <b>mask</b> elements
* Reading <b>image</b> currently not supported
* Reading <b>text</b> on path

<br>

## About Open CASCADE Technology (OCCT)
The complete distribution can be cloned from the https://github.com/Open-Cascade-SAS/OCCT.
This allows you to use additional parts, build the library with different options or make code changes. The currently used version can be found in the about dialog.

<br>

# Software Description

## Start Application
Before you start the application, make sure that the Occt.NET dll is copied into the Application’s debug or release folder.<br>

<b>From:</b> OCCSVGViewer\bin\Debug\net8.0-windows\occt\x64\
<br>
<img width="526" height="236" alt="image" src="https://github.com/user-attachments/assets/53b8efb8-0bd1-4f72-9828-07cb6a4a26c6" />
<br><br>

<b>To:</b> OCCSVGViewer\bin\Debug\net8.0-windows\
<br>
<img width="522" height="269" alt="image" src="https://github.com/user-attachments/assets/b3f2fc2a-8df1-4176-b267-abfed4a1863b" />
<br>

## Unit pixel
The default unit is currently pixels. Later, user will be able to choose their own units, such as inches, points, centimeters or millimeters.<br>

<img width="555" height="156" alt="image" src="https://github.com/user-attachments/assets/497a055b-8417-4189-892d-31a149e5fad2" />
<br>
<br>

## Display mode (Wire or Shaded)
* <b>Wire</b>: The SVG elements will be displayed in Wireframe and it's no longer possible to show them in shaded mode. Here you can handle the element as a Curves model, which reduces the computational effort to display them.
* <b>Shaded:</b> In "Shaded mode", the elements retain their original background color (stored in the SVG file). You can handle the element here as Face and Curves model.

<br>
<img width="531" height="347" alt="image" src="https://github.com/user-attachments/assets/7ef896bb-4417-4a27-9fb8-bb23a1f76210" />
<br>
<img width="945" height="578" alt="image" src="https://github.com/user-attachments/assets/cfb32248-b26e-477d-87c4-03db597eb84b" />
<br>

## Topology Model 
<div>Topology optimization is a fast and easy way to maximize a park’s performance based on a set of constraints. </div><br>
<div>In a topology model, maintain clarity by creating single, self-contained bodies with their modeling histories, arranged as modifier stacks. These bodies can them interact with each other. For a clean workspace, sort them into layers.
</div>
<br>
<img width="472" height="645" alt="image" src="https://github.com/user-attachments/assets/1d577764-37a6-4c91-8cb9-c3fb3e2e2af0" />
<br>

## Show report
The report shows a system log file that lists all information about the model's load, including failed loads and bugs.<br>

<img width="940" height="577" alt="image" src="https://github.com/user-attachments/assets/a3b9f1b3-d4e5-4e25-aa31-8eec5b4d8e85" />
<br>

## For Developer

# Standard code:
<br>
<img width="571" height="357" alt="image" src="https://github.com/user-attachments/assets/881ad13c-b37e-4f67-901e-84e61913dd04" />
<br>


<code color="gray">
// Handling of shape outside of an OCCSVG.NET
OCCElement doc = reader.Open(fileName); 

foreach (var element in doc.Children)
{
  if (element.TopoShape == null) continue;
  // Create AIS_Shape for visualization
  AIS_Shape aisShape = new AIS_Shape(element.TopoShape);
  this.View.DisplayShape(aisShape);
}
</code>

<code color="gray">
// Get elements by class name
List<PathSegment> paths = doc.GetAllElements<PathSegment>();
List<Rectangle> Rectangles = doc.GetAllElements<Rectangle>();
</code>

<code color="gray"> 
// Get element by selected node name
TreeNode node = this.ModelTreeView.SelectedNode;
if (node != null)
{
 AOCCBaseElement element = doc.GetElementByName(node.Name);
}
</code>
<br>
