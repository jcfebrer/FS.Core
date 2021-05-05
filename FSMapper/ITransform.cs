namespace XML2XML
{
	public interface ITransform
	{
		System.Xml.XmlDocument InputXmlDocument { get; }
		System.String Output { get; }
		System.String Transform(System.Xml.XmlDocument sourceXmlDoc);
	}
}