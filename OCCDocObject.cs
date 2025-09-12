/*******************************************************************************
*                                                                              
* Author    :  Rabih Kaddour      
* 
* Date      :  15 July 2023       
* 
* Copyright :  Rabih Kaddour  2023-2024
*              Copyright (C) Rabih Kaddour. All rights reserved.
* 
* File      :  OCCDocObject.cs     
* 
* Content   :  Represents the OCC properties.
* 
* License:                                                                     
* Use, modification & distribution is subject to Boost Software License Ver 1. 
* http://www.boost.org/LICENSE_1_0.txt                                         
*                                                                              
* Website:  https://inovaitec.com                                              
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCCSVGViewer
{
    /// <summary>
    /// Open CASCADE supports a wide range of CAD formats.
    /// Supported formats include STEP, IGES, BREP, glTF, JT, PLY, STL, OBJ, and 3DM for CAD and mesh data.
    /// The possible model formats for import and export in OCCT.
    /// </summary>
    public enum ModelFormat
    {
        /// <summary>
        /// The Boundary Representation Format, which describes the geometry of a 3D model
        /// through its surfaces and edges. 
        /// BREP models combine topological information (e.g., faces, edges, vertices) 
        /// with geometric data (e.g., surfaces, curves, points). 
        /// </summary>
        /// <remarks>
        /// OCCT uses BREP as the foundation for its internal
        /// data structures.
        /// </remarks>
        BREP,

        /// <summary>
        /// Run-Length Encoding CAD-format. OC internal model. 
        /// </summary>
        RLE,

        /// <summary>
        /// CSFDB (Component Storage Format Database) - OC internal model.
        /// CSFDB is designed for storing complex 3D models, including their geometry, topology
        /// and associated data within the OCCT environment. 
        /// </summary>
        /// <remarks>
        /// Relationship to BREP:
        /// CSFDB is closely related to the BREP (Boundary Representation) data structure,
        /// which is a fundamental way of representing 3D geometry in OCCT. 
        /// BREP data is typically stored within the CSFDB format.
        /// </remarks>
        CSFDB,

        /// <summary>
        /// Standard for the Exchange of Product Model Data - ISO 10303.
        /// A widely used format for exchanging 3D models between different CAD systems.
        /// OCCT supports both BRep (Boundary Representation) and mesh data in STEP. 
        /// </summary>
        STEP,

        /// <summary>
        /// Standard for the Exchange of Product Model Data - ISO 10303.
        /// A widely used format for exchanging 3D models between different CAD systems.
        /// OCCT supports both BRep (Boundary Representation) and mesh data in STEP. 
        /// </summary>
        STP,

        /// <summary>
        /// The Initial Graphics Exchange Specification - IGES.
        /// </summary>
        /// <remarks>
        ///  IGES entities, including:
        ///  - Points: Individual points in space.
        ///  - Lines: Straight lines.
        ///  - Curves: Various types of curves such as arcs, cone curves, spline curves, and B-spline curves.
        ///  - Surfaces: Various surfaces such as planes, cylinders, spheres, and surfaces that are bounded by curves.
        ///  - B-Rep entities: Topological elements such as edges, faces, and solids.
        ///  - Structured entities: Groups and subfigures used to organize entities.
        /// </remarks>
        IGES,

        /// <summary>
        /// The Initial Graphics Exchange Specification - IGES.
        /// </summary>
        /// <remarks>
        ///  IGES entities, including:
        ///  - Points: Individual points in space.
        ///  - Lines: Straight lines.
        ///  - Curves: Various types of curves such as arcs, cone curves, spline curves, and B-spline curves.
        ///  - Surfaces: Various surfaces such as planes, cylinders, spheres, and surfaces that are bounded by curves.
        ///  - B-Rep entities: Topological elements such as edges, faces, and solids.
        ///  - Structured entities: Groups and subfigures used to organize entities.
        /// </remarks>
        IGS,

        /// <summary>
        /// Virtual Reality Modeling Language - polygones, VRML.
        /// </summary>
        VRML,

        /// <summary>
        /// Stereolitography - triangles, STL.
        /// A common format for 3D printing and CAD. 
        /// STL files represent the surface geometry of a 3D object using a mesh of triangles, 
        /// without including color or other attributes
        /// </summary>
        STL,

        /// <summary>
        /// OCCT supports destination image files (.png, .bmp, .jpg).
        /// </summary>
        IMAGE,

        /// <summary>
        /// A DXF file is Drawing Exchange Format file, primarily used for exchanging CAD 
        /// (Computer-Aided Design) data between different CAD software applications.
        /// </summary>
        DXF,

        /// <summary>
        /// Undefined format.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Summary description for OCCDocObject.
    /// </summary>
    public class OCCDocObject
    {
        #region Private Fields

        /// <summary>
        /// The parameter OCCViewer.
        /// </summary>
        private OCCViewer myView;

        /// <summary>
        /// The parameter OCCDoc.
        /// </summary>
        private OCCDoc myDoc;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the OCCViewer object.
        /// </summary>
        public OCCViewer View
        {
            get
            {
                return this.myView;
            }
            set
            {
                this.myView = value;
            }
        }

        /// <summary>
        /// Gets or sets the OCCDoc object.
        /// </summary>
        public OCCDoc Doc
        {
            get
            {
                return this.myDoc;
            }
            set
            {
                this.myDoc = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OCCDocObject"/> class.
        /// </summary>
        public OCCDocObject()
        {
        }

        #endregion
    }
}
