﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace meeting.Model
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="meeting")]
	public partial class UserDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertT_user(T_user instance);
    partial void UpdateT_user(T_user instance);
    partial void DeleteT_user(T_user instance);
    partial void InsertT_system(T_system instance);
    partial void UpdateT_system(T_system instance);
    partial void DeleteT_system(T_system instance);
    #endregion
		
		public UserDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public UserDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UserDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UserDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UserDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<T_user> T_users
		{
			get
			{
				return this.GetTable<T_user>();
			}
		}
		
		public System.Data.Linq.Table<T_system> T_systems
		{
			get
			{
				return this.GetTable<T_system>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_user")]
	public partial class T_user : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _u_id;
		
		private string _u_username;
		
		private string _u_password;
		
		private string _u_actor;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onu_idChanging(int value);
    partial void Onu_idChanged();
    partial void Onu_usernameChanging(string value);
    partial void Onu_usernameChanged();
    partial void Onu_passwordChanging(string value);
    partial void Onu_passwordChanged();
    partial void Onu_actorChanging(string value);
    partial void Onu_actorChanged();
    #endregion
		
		public T_user()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_u_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int u_id
		{
			get
			{
				return this._u_id;
			}
			set
			{
				if ((this._u_id != value))
				{
					this.Onu_idChanging(value);
					this.SendPropertyChanging();
					this._u_id = value;
					this.SendPropertyChanged("u_id");
					this.Onu_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_u_username", DbType="NVarChar(50)")]
		public string u_username
		{
			get
			{
				return this._u_username;
			}
			set
			{
				if ((this._u_username != value))
				{
					this.Onu_usernameChanging(value);
					this.SendPropertyChanging();
					this._u_username = value;
					this.SendPropertyChanged("u_username");
					this.Onu_usernameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_u_password", DbType="NVarChar(50)")]
		public string u_password
		{
			get
			{
				return this._u_password;
			}
			set
			{
				if ((this._u_password != value))
				{
					this.Onu_passwordChanging(value);
					this.SendPropertyChanging();
					this._u_password = value;
					this.SendPropertyChanged("u_password");
					this.Onu_passwordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_u_actor", DbType="NVarChar(50)")]
		public string u_actor
		{
			get
			{
				return this._u_actor;
			}
			set
			{
				if ((this._u_actor != value))
				{
					this.Onu_actorChanging(value);
					this.SendPropertyChanging();
					this._u_actor = value;
					this.SendPropertyChanged("u_actor");
					this.Onu_actorChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_system")]
	public partial class T_system : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _s_id;
		
		private string _s_version;
		
		private string _s_server_address;
		
		private string _s_port;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ons_idChanging(int value);
    partial void Ons_idChanged();
    partial void Ons_versionChanging(string value);
    partial void Ons_versionChanged();
    partial void Ons_server_addressChanging(string value);
    partial void Ons_server_addressChanged();
    partial void Ons_portChanging(string value);
    partial void Ons_portChanged();
    #endregion
		
		public T_system()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_s_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int s_id
		{
			get
			{
				return this._s_id;
			}
			set
			{
				if ((this._s_id != value))
				{
					this.Ons_idChanging(value);
					this.SendPropertyChanging();
					this._s_id = value;
					this.SendPropertyChanged("s_id");
					this.Ons_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_s_version", DbType="NVarChar(20)")]
		public string s_version
		{
			get
			{
				return this._s_version;
			}
			set
			{
				if ((this._s_version != value))
				{
					this.Ons_versionChanging(value);
					this.SendPropertyChanging();
					this._s_version = value;
					this.SendPropertyChanged("s_version");
					this.Ons_versionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_s_server_address", DbType="NVarChar(50)")]
		public string s_server_address
		{
			get
			{
				return this._s_server_address;
			}
			set
			{
				if ((this._s_server_address != value))
				{
					this.Ons_server_addressChanging(value);
					this.SendPropertyChanging();
					this._s_server_address = value;
					this.SendPropertyChanged("s_server_address");
					this.Ons_server_addressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_s_port", DbType="NVarChar(20)")]
		public string s_port
		{
			get
			{
				return this._s_port;
			}
			set
			{
				if ((this._s_port != value))
				{
					this.Ons_portChanging(value);
					this.SendPropertyChanging();
					this._s_port = value;
					this.SendPropertyChanged("s_port");
					this.Ons_portChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
