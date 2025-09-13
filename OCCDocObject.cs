/*******************************************************************************
*                                                                              
* Author    :  Rabih Kaddour      
* 
* Date      :  15 July 2023       
* 
* Copyright :  Rabih Kaddour  2023-2025
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
