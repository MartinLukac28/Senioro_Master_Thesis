using System.Collections.Generic;
using System.Xml;

public class XmlNodeComparer : IComparer<XmlNode>
{
    private string sortExpression;

    public XmlNodeComparer(string sortExpression)
    {
        this.sortExpression = sortExpression;
    }

    public int Compare(XmlNode x, XmlNode y)
    {
        string[] sortProps = sortExpression.Split(' ');
        string sortProp = sortProps[0];
        string sortOrder = sortProps[1];

        if (sortOrder == "asc")
        {
            return x.SelectSingleNode(sortProp).InnerText.CompareTo(y.SelectSingleNode(sortProp).InnerText);
        }
        else
        {
            return y.SelectSingleNode(sortProp).InnerText.CompareTo(x.SelectSingleNode(sortProp).InnerText);
        }
    }
}