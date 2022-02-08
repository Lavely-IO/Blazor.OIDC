// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="JwtKey.cs" company="LavelyIO">
//     Copyright (c) LavelyIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC.Config
{
    /// <summary>
    /// Class JwtKey.
    /// </summary>
    public class JwtKey
    {
        /// <summary>
        /// Gets or sets the kty.
        /// </summary>
        /// <value>The kty.</value>
        public string kty { get; set; }

        /// <summary>
        /// Gets or sets the use.
        /// </summary>
        /// <value>The use.</value>
        public string use { get; set; }

        /// <summary>
        /// Gets or sets the kid.
        /// </summary>
        /// <value>The kid.</value>
        public string kid { get; set; }

        /// <summary>
        /// Gets or sets the X5T.
        /// </summary>
        /// <value>The X5T.</value>
        public string x5t { get; set; }

        /// <summary>
        /// Gets or sets the n.
        /// </summary>
        /// <value>The n.</value>
        public string n { get; set; }

        /// <summary>
        /// Gets or sets the e.
        /// </summary>
        /// <value>The e.</value>
        public string e { get; set; }

        /// <summary>
        /// Gets or sets the X5C.
        /// </summary>
        /// <value>The X5C.</value>
        public List<string> x5c { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        public string issuer { get; set; }
    }
}