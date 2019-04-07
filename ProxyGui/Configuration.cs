/*
 * Created by SharpDevelop.
 * User: Alejandro
 * Date: 06/30/2015
 * Time: 10:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;

namespace ProxyGui
{
	/// <summary>
	/// Description of Configuration.
	/// </summary>
	public class Configuration
	{
		XmlDocument xml;
		
		public Configuration()
		{
		}
		
		public Configuration(string XmlFile)
		{
			try
			{
				xml = new XmlDocument();
				xml.Load(XmlFile);
			}
			catch(FileNotFoundException ex)
			{
				throw new FileNotFoundException(@"{0} não foi encontrado.", ex);
			}
		}
		
		public XmlNodeList GetList()
		{
			XmlNode node = xml.DocumentElement.FirstChild;
			//XmlNodeList xnList = xml.SelectNodes("Config/IpList");
			XmlNodeList xnList = node.ChildNodes;
			return xnList;
		}
	}
}
