﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AntAttack.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AntAttack.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 2       #Level count
        ///===Level 1===
        ///39 20 0 #Rescuer&apos;s position
        ///9 25 4  #Rescuee&apos;s position
        ///3       #Ant Count
        ///10 20 0 #Ants
        ///24 10 0
        ///5 13 0 
        ///===Level 2===
        ///39 20 0 #Rescuer&apos;s position
        ///10 14 0 #Rescuee&apos;s position
        ///5       #Ant Count
        ///10 20 0 #Ants
        ///24 10 0
        ///5 13 0 
        ///35 23 0 
        ///13 32 0 
        ///.
        /// </summary>
        public static string levels {
            get {
                return ResourceManager.GetString("levels", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 40
        ///40
        ///6
        ///
        ///XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X............................X.........X
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X......................................X
        ///X.......... [rest of string was truncated]&quot;;.
        /// </summary>
        public static string map {
            get {
                return ResourceManager.GetString("map", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        public static System.Drawing.Bitmap sprites {
            get {
                object obj = ResourceManager.GetObject("sprites", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
