using System.Configuration;

namespace FSLibraryCore
{
    /// <summary>
    /// Settings personalizados.
    /// </summary>
    public class FSSettingsSection : ConfigurationSection
    {
        /// <summary>
        /// Settings
        /// </summary>
        [ConfigurationProperty("Settings")]
        public FSSettingsCollection Settings
        {
            get { return this["Settings"] as FSSettingsCollection; }
            set { this["Settings"] = value; }
        }

        /// <summary>
        /// Retornamos false indicando que no es de solo lectura.
        /// </summary>
        /// <returns></returns>
        public override bool IsReadOnly()
        {
            return false;
        }
    }

    /// <summary>
    /// Colección de FSSettingsElement
    /// </summary>
    [ConfigurationCollection(typeof(FSSettingsElement))]
    public class FSSettingsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Añadir
        /// </summary>
        /// <param name="element"></param>
        public void Add(FSSettingsElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Añadir
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            BaseAdd(new FSSettingsElement(key, value), false);
        }

        /// <summary>
        /// Añadir
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        public void Add(string key, string value, string description)
        {
            BaseAdd(new FSSettingsElement(key, value, description), false);
        }

        /// <summary>
        /// Añadir
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        public void Add(string key, string value, string description, string type)
        {
            BaseAdd(new FSSettingsElement(key, value, description, type), false);
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="element"></param>
        public void Remove(FSSettingsElement element)
        {
            BaseRemove(element);
        }

        /// <summary>
        /// Eliminar
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            BaseRemove(key);
        }

        /// <summary>
        /// Borrar
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// this
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public FSSettingsElement this[int index]
        {
            get { return (FSSettingsElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// this
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new FSSettingsElement this[string key]
        {
            get { return (FSSettingsElement)BaseGet(key); }
            set
            {
                if (BaseGet(key) != null)
                    BaseRemove(key);

                BaseAdd(this.Count, value);
            }
        }

        /// <summary>
        /// Crear nuevo elemento
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FSSettingsElement();
        }

        /// <summary>
        /// Obetner la clave del elemento
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FSSettingsElement)element).Key;
        }

        /// <summary>
        /// Retornamos false indicando que no es de solo lectura.
        /// </summary>
        /// <returns></returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Indica si un elemento esta modificado.
        /// </summary>
        /// <returns></returns>
        protected override bool IsModified()
        {
            return true; // base.IsModified() || this.Cast<FSSettingsElement>().Any(e => e.HasChanges());
        }
    }

    /// <summary>
    /// FSSettingsElement
    /// </summary>
    public class FSSettingsElement : ConfigurationElement
    {
        /// <summary>
        /// Constructor vacio
        /// </summary>
        public FSSettingsElement()
        {
        }

        /// <summary>
        /// Retornamos false indicando que no es de solo lectura.
        /// </summary>
        /// <returns></returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Contructor con par de clave/valor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public FSSettingsElement(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        public FSSettingsElement(string key, string value, string description) : this(key, value)
        {
            Description = description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        public FSSettingsElement(string key, string value, string description, string type) : this(key, value, description)
        {
            Type = type;
        }

        /// <summary>
        /// Clave
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get { return (string)base["key"]; }
            set { base["key"] = value; }
        }

        /// <summary>
        /// Valor
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)base["value"]; }
            set { base["value"] = value; }
        }

        /// <summary>
        /// Descripción
        /// </summary>
        [ConfigurationProperty("desc", IsRequired = false)]
        public string Desc
        {
            get { return (string)base["desc"]; }
            set { base["desc"] = value; }
        }

        /// <summary>
        /// Descripción
        /// </summary>
        [ConfigurationProperty("description", IsRequired = false)]
        public string Description
        {
            get { return (string)base["description"]; }
            set { base["description"] = value; }
        }

        /// <summary>
        /// Tipo
        /// </summary>
        [ConfigurationProperty("type", IsRequired = false)]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
