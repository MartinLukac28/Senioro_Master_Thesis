using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace Senioro_1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://microsoft.com/webservices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {


        [WebMethod]
        public string SearchEntry(string id)
        {
            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNodeList nodes = xmlDoc.SelectNodes("//record[id='" + id + "']");

            if (nodes != null && nodes.Count > 0)
            {
                XmlNode recordNode = nodes[0];
                return recordNode.OuterXml;
            }
            else
            {
                return "Entry not found";
            }
        }

        [WebMethod]
        public static string CalculateAges()
        {
            string path = HttpContext.Current.Server.MapPath("~/xml/XMLFile1.xml");
            XDocument xmlDoc = XDocument.Load(path);

            foreach (XElement record in xmlDoc.Descendants("record"))
            {
                XElement ageElement = record.Element("age");
                if (ageElement != null)
                {
                    // Age element already exists, update its value
                    // Age element already exists, update its valueread
                    string dobString = record.Element("dateOfBirth").Value;
                    DateTime dob = DateTime.ParseExact(dobString, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    int age = DateTime.Today.Year - dob.Year;
                    if (DateTime.Today < dob.AddYears(age)) age--;
                    ageElement.SetValue(age.ToString());
                }
                else
                {
                    // Age element doesn't exist, add a new one
                    string dobString = record.Element("dateOfBirth").Value;
                    DateTime dob = DateTime.ParseExact(dobString, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    int age = DateTime.Today.Year - dob.Year;
                    if (DateTime.Today < dob.AddYears(age)) age--;
                    ageElement = new XElement("age", age);
                    record.Add(ageElement);
                }
            }

            xmlDoc.Save(path);

            return "true";
        }

        [WebMethod]
        public string AddNewEntry(string firstName, string lastName, string dateOfBirth, string phoneNumber,
                        string gender, string monthPayment, string pension, string roomNo, string diagnose1, string diagnose2, 
                        string immobility, string dependency, string dateOfArrival,
                        string contactName, string contactLastName, string contactPhoneNumber, string diagnose3 = null, string dateOfDeparture = null)
        {
            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlElement root = xmlDoc.DocumentElement;

            XmlElement newRecord = xmlDoc.CreateElement("record");

            XmlElement newId = xmlDoc.CreateElement("id");
            int maxId = 1;
            foreach (XmlNode node in root.ChildNodes)
            {
                int Id = Convert.ToInt32(node["id"].InnerText);
                if (Id >= maxId)
                {
                    maxId = Id + 1;
                }
            }
            newId.InnerText = maxId.ToString();
            newRecord.AppendChild(newId);

            XmlElement newFirstName = xmlDoc.CreateElement("firstName");
            newFirstName.InnerText = firstName;
            newRecord.AppendChild(newFirstName);

            XmlElement newLastName = xmlDoc.CreateElement("lastName");
            newLastName.InnerText = lastName;
            newRecord.AppendChild(newLastName);

            XmlElement newDateOfBirth = xmlDoc.CreateElement("dateOfBirth");
            newDateOfBirth.InnerText = dateOfBirth;
            newRecord.AppendChild(newDateOfBirth);

            XmlElement newPhoneNumber = xmlDoc.CreateElement("phoneNumber");
            newPhoneNumber.InnerText = phoneNumber;
            newRecord.AppendChild(newPhoneNumber);

            XmlElement newGender = xmlDoc.CreateElement("gender");
            newGender.InnerText = gender;
            newRecord.AppendChild(newGender);

            XmlElement newMonthPayment = xmlDoc.CreateElement("monthPayment");
            newMonthPayment.InnerText = monthPayment;
            newRecord.AppendChild(newMonthPayment);

            XmlElement newPension = xmlDoc.CreateElement("pension");
            newPension.InnerText = pension;
            newRecord.AppendChild(newPension);

            XmlElement newRoomNo = xmlDoc.CreateElement("roomNo");
            newRoomNo.InnerText = roomNo;
            newRecord.AppendChild(newRoomNo);

            XmlElement newDiagnose1 = xmlDoc.CreateElement("diagnose1");
            newDiagnose1.InnerText = diagnose1;
            newRecord.AppendChild(newDiagnose1);

            XmlElement newDiagnose2 = xmlDoc.CreateElement("diagnose2");
            if (!string.IsNullOrEmpty(diagnose2))
            {
                newDiagnose2.InnerText = diagnose2;
            }
            newRecord.AppendChild(newDiagnose2);

            XmlElement newDiagnose3 = xmlDoc.CreateElement("diagnose3");
            if (!string.IsNullOrEmpty(diagnose3))
            {
                newDiagnose3.InnerText = diagnose3;
            }
            newRecord.AppendChild(newDiagnose3);

            XmlElement newImmobility = xmlDoc.CreateElement("immobility");
            newImmobility.InnerText = immobility;
            newRecord.AppendChild(newImmobility);

            XmlElement newDependency = xmlDoc.CreateElement("dependency");
            newDependency.InnerText = dependency;
            newRecord.AppendChild(newDependency);

            XmlElement newDateOfArrival = xmlDoc.CreateElement("dateOfArrival");
            newDateOfArrival.InnerText = dateOfArrival;
            newRecord.AppendChild(newDateOfArrival);

            XmlElement newDateOfDeparture = xmlDoc.CreateElement("dateOfDeparture");
            if (!string.IsNullOrEmpty(dateOfDeparture))
            {
                newDateOfDeparture.InnerText = dateOfDeparture;
            }
            newRecord.AppendChild(newDateOfDeparture);

            XmlElement newContactPerson = xmlDoc.CreateElement("contactPerson");

            XmlElement newContactName = xmlDoc.CreateElement("name");
            newContactName.InnerText = contactName;
            newContactPerson.AppendChild(newContactName);

            XmlElement newContactLastName = xmlDoc.CreateElement("lastName");
            newContactLastName.InnerText = contactLastName;
            newContactPerson.AppendChild(newContactLastName);

            XmlElement newContactPhoneNumber = xmlDoc.CreateElement("phoneNumber");
            newContactPhoneNumber.InnerText = contactPhoneNumber;
            newContactPerson.AppendChild(newContactPhoneNumber);

            newRecord.AppendChild(newContactPerson);

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(phoneNumber) ||
    string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(monthPayment) || string.IsNullOrEmpty(pension) || string.IsNullOrEmpty(roomNo) ||
    string.IsNullOrEmpty(diagnose1) || string.IsNullOrEmpty(immobility) || string.IsNullOrEmpty(dependency) || string.IsNullOrEmpty(dateOfArrival) ||
    string.IsNullOrEmpty(contactName) || string.IsNullOrEmpty(contactLastName) || string.IsNullOrEmpty(contactPhoneNumber))
            {
                return "false";
            }

            root.AppendChild(newRecord);

            xmlDoc.Save(path);

            return "New entry added";
        }

        [WebMethod]
        public string UpdateEntry(string id, string firstName = null, string lastName = null, string dateOfBirth = null, string phoneNumber = null,
                    string gender = null, string monthPayment = null, string pension = null, string roomNo = null, string diagnose1 = null, string diagnose2 = null, string diagnose3 = null,
                    string immobility = null, string dependency = null, string dateOfArrival = null, string dateOfDeparture = null,
                    string contactName = null, string contactLastName = null, string contactPhoneNumber = null)
        {
            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("record[id='" + id + "']");
            if (nodes != null && nodes.Count > 0)
            {
                XmlNode recordNode = nodes[0];
                foreach (XmlNode childNode in recordNode.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "firstName":
                            if (!string.IsNullOrEmpty(firstName))
                                childNode.InnerText = firstName;
                            break;
                        case "lastName":
                            if (!string.IsNullOrEmpty(lastName))
                                childNode.InnerText = lastName;
                            break;
                        case "dateOfBirth":
                            if (!string.IsNullOrEmpty(dateOfBirth))
                                childNode.InnerText = dateOfBirth;
                            break;
                        case "phoneNumber":
                            if (!string.IsNullOrEmpty(phoneNumber))
                                childNode.InnerText = phoneNumber;
                            break;
                        case "gender":
                            if (!string.IsNullOrEmpty(gender))
                                childNode.InnerText = gender;
                            break;
                        case "monthPayment":
                            if (!string.IsNullOrEmpty(monthPayment))
                                childNode.InnerText = monthPayment;
                            break;
                        case "pension":
                            if (!string.IsNullOrEmpty(pension))
                                childNode.InnerText = pension;
                            break;
                        case "roomNo":
                            if (!string.IsNullOrEmpty(roomNo))
                                childNode.InnerText = roomNo;
                            break;
                        case "diagnose1":
                            if (!string.IsNullOrEmpty(diagnose1))
                                childNode.InnerText = diagnose1;
                            break;
                        case "diagnose2":
                            if (!string.IsNullOrEmpty(diagnose2))
                                childNode.InnerText = diagnose2;
                            break;
                        case "diagnose3":
                            if (!string.IsNullOrEmpty(diagnose3))
                                childNode.InnerText = diagnose3;
                            break;
                        case "immobility":
                            if (!string.IsNullOrEmpty(immobility))
                                childNode.InnerText = immobility;
                            break;
                        case "dependency":
                            if (!string.IsNullOrEmpty(dependency))
                                childNode.InnerText = dependency;
                            break;
                        case "dateOfArrival":
                            if (!string.IsNullOrEmpty(dateOfArrival))
                                childNode.InnerText = dateOfArrival;
                            break;
                        case "dateOfDeparture":
                            if (!string.IsNullOrEmpty(dateOfDeparture))
                                childNode.InnerText = dateOfDeparture;
                            break;
                        case "contactPerson":
                            foreach (XmlNode contactNode in childNode.ChildNodes)
                            {
                                switch (contactNode.Name)
                                {
                                    case "name":
                                        if (!string.IsNullOrEmpty(contactName))
                                            contactNode.InnerText = contactName;
                                        break;
                                    case "lastName":
                                        if (!string.IsNullOrEmpty(contactLastName))
                                            contactNode.InnerText = contactLastName;
                                        break;
                                    case "phoneNumber":
                                        if (!string.IsNullOrEmpty(contactPhoneNumber))
                                            contactNode.InnerText = contactPhoneNumber;
                                        break;
                                }
                            }
                            break;
                    }
                }
                xmlDoc.Save(path);
                return "Entry updated";
            }
            else
            {
                // Return an error message
                return "Error updating record with id " + id + ".";
            }

        }

        [WebMethod]
        public string DeleteEntry(string id)
        {
            // Load the XML file
            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            // Find the record with the specified id
            XmlNode recordToDelete = xmlDoc.SelectSingleNode($"//record[id={id}]");

            if (recordToDelete != null)
            {
                // Remove the record from the XML file
                XmlNode root = xmlDoc.DocumentElement;
                root.RemoveChild(recordToDelete);

                // Save the updated XML file
                xmlDoc.Save(path);

                // Return a success message
                return "Entry deleted";
            }
            else
            {
                // Return an error message
                return "Error deleting record with id " + id + ".";
            }
        }

        [WebMethod]
        public XmlDocument FilterClients(string filterBy = null, string filterValue = null, string dateOfArrival = null, string dateOfDeparture = null, string sortBy = null, string sortOrder = null, string diagnose1 = null, string diagnose2 = null, string diagnose3 = null)
        {
            CalculateAges();
            // Load the XML file from disk
            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            // Get the root element
            XmlElement root = xmlDoc.DocumentElement;

            // Filter the client records based on the specified property and value
            XmlNodeList nodes = root.SelectNodes("record");
            List<XmlNode> filteredList = new List<XmlNode>();
            foreach (XmlNode node in nodes)
            {
                bool shouldInclude = true;
                if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
                {
                    if (node.SelectSingleNode(filterBy).InnerText.Contains(filterValue))
                    {
                        shouldInclude = true;
                    }
                    else
                    {
                        shouldInclude = false;
                    }
                }

                if (!string.IsNullOrEmpty(dateOfArrival))
                {
                    DateTime arrival;
                    if (DateTime.TryParseExact(node.SelectSingleNode("dateOfArrival").InnerText, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrival))
                    {
                        shouldInclude &= arrival <= DateTime.ParseExact(dateOfArrival, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        shouldInclude = false;
                    }
                }

                if (!string.IsNullOrEmpty(node.SelectSingleNode("dateOfDeparture").InnerText) && !string.IsNullOrEmpty(dateOfDeparture))
                {
                    DateTime departure;
                    if (DateTime.TryParseExact(node.SelectSingleNode("dateOfDeparture").InnerText, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out departure))
                    {
                        shouldInclude &= departure >= DateTime.ParseExact(dateOfDeparture, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        shouldInclude = false;
                    }
                }
                               
                if (!string.IsNullOrEmpty(diagnose1))
                {
                    shouldInclude &= node.SelectSingleNode("diagnose1").InnerText.Equals(diagnose1) ||
                        node.SelectSingleNode("diagnose2").InnerText.Equals(diagnose1) ||
                        node.SelectSingleNode("diagnose3").InnerText.Equals(diagnose1);
                }

                if (!string.IsNullOrEmpty(diagnose2))
                {
                    shouldInclude &= node.SelectSingleNode("diagnose1").InnerText.Equals(diagnose2) ||
                        node.SelectSingleNode("diagnose2").InnerText.Equals(diagnose2) ||
                        node.SelectSingleNode("diagnose3").InnerText.Equals(diagnose2);
                }

                if (!string.IsNullOrEmpty(diagnose3))
                {
                    shouldInclude &= node.SelectSingleNode("diagnose1").InnerText.Equals(diagnose3) ||
                        node.SelectSingleNode("diagnose2").InnerText.Equals(diagnose3) ||
                        node.SelectSingleNode("diagnose3").InnerText.Equals(diagnose3);
                }

                if (shouldInclude)
                {
                    filteredList.Add(node);
                }
            }

            // Sort the filtered client records based on the specified property
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortOrder))
            {
                string sortExpression = string.Format("{0} {1}", sortBy, sortOrder);
                filteredList.Sort(new XmlNodeComparer(sortExpression));
            }

            // Create a new XML document to hold the sorted and filtered client records
            XmlDocument sortedXmlDoc = new XmlDocument();
            sortedXmlDoc.LoadXml("<Client></Client>");
            XmlElement sortedRoot = sortedXmlDoc.DocumentElement;

            // Add the sorted and filtered client records to the new XML document
            foreach (XmlNode node in filteredList)
            {
                XmlNode importedNode = sortedXmlDoc.ImportNode(node, true);
                sortedRoot.AppendChild(importedNode);
            }

            // Create a new XML document to hold the result
            XmlDocument resultXmlDoc = new XmlDocument();
            resultXmlDoc.LoadXml("<Result></Result>");
            XmlElement resultRoot = resultXmlDoc.DocumentElement;

            // Add the number of filtered entries to the result
            XmlElement countElement = resultXmlDoc.CreateElement("Count");
            countElement.InnerText = string.Format("{0} of the entries fulfil the given conditions", filteredList.Count);
            resultRoot.AppendChild(countElement);

           // Add the sorted and filtered client records to the result
            XmlNode importedSortedNode = resultXmlDoc.ImportNode(sortedRoot, true);
            resultRoot.AppendChild(importedSortedNode);
           
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/filter"), "Filter" + "_" + filterBy + "_" + filterValue + "_" + diagnose1 + diagnose2 + diagnose3 + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
                resultXmlDoc.Save(filePath);
            

            // Return the result as an XML document
            return resultXmlDoc;
        }

        [WebMethod]
        public XmlDocument SortClientsAtStart(int year, string sortBy = null, string sortOrder = null)
        {
            CalculateAges();
            int count = 0;
            XmlDocument result = new XmlDocument();

            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode root = result.CreateElement("Client");
                result.AppendChild(root);

                XmlNodeList records = xmlDoc.GetElementsByTagName("record");
                List<XmlNode> filteredRecords = new List<XmlNode>();
                foreach (XmlNode record in records)
                {
                    XmlNode dateOfArrivalNode = record.SelectSingleNode("dateOfArrival");
                    DateTime dateOfArrival = DateTime.Parse(dateOfArrivalNode.InnerText);
                    if (dateOfArrival <= new DateTime(year, 1, 1))
                    {
                        XmlNode dateOfDepartureNode = record.SelectSingleNode("dateOfDeparture");
                        if (dateOfDepartureNode != null && !string.IsNullOrEmpty(dateOfDepartureNode.InnerText))
                        {
                            DateTime dateOfDeparture;
                            if (DateTime.TryParse(dateOfDepartureNode.InnerText, out dateOfDeparture))
                            {
                                if (dateOfDeparture >= new DateTime(year, 1, 1))
                                {
                                    filteredRecords.Add(record);
                                    count++;
                                }
                            }
                        }
                        else
                        {
                            filteredRecords.Add(record);
                            count++;
                        }
                    }
                }

                // Sort the filtered client records based on the specified property
                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortOrder))
                {
                    string sortExpression = string.Format("{0} {1}", sortBy, sortOrder);
                    filteredRecords.Sort(new XmlNodeComparer(sortExpression));
                }

                // Create a new XML document to hold the sorted and filtered client records
                XmlDocument sortedXmlDoc = new XmlDocument();
                sortedXmlDoc.LoadXml("<result></result>");
                XmlElement sortedRoot = sortedXmlDoc.DocumentElement;

            foreach (XmlNode node in filteredRecords)
            {
                XmlNode importedNode = sortedXmlDoc.ImportNode(node, true);
                sortedRoot.AppendChild(importedNode);
            }

            // Add the count node to the result document
            XmlNode countNode = sortedXmlDoc.CreateElement("Count");
            countNode.InnerText = count.ToString();
            sortedRoot.InsertBefore(countNode, sortedRoot.FirstChild);

            // Set the sorted and filtered client records as the result
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/stats"), "Stats_Start_Year" + "_" + sortBy + "_" + sortOrder + "_"  + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
            sortedXmlDoc.Save(filePath);
            return sortedXmlDoc;
        }

        [WebMethod]
        public XmlDocument SortClientsAtEnd(int year, string sortBy = null, string sortOrder = null)
        {
            CalculateAges();
            int count = 0;
            XmlDocument result = new XmlDocument();

            string path = Server.MapPath("~/xml/XMLFile1.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNode root = result.CreateElement("Client");
            result.AppendChild(root);

            XmlNodeList records = xmlDoc.GetElementsByTagName("record");
            List<XmlNode> filteredRecords = new List<XmlNode>();
            foreach (XmlNode record in records)
            {
                XmlNode dateOfArrivalNode = record.SelectSingleNode("dateOfArrival");
                DateTime dateOfArrival = DateTime.Parse(dateOfArrivalNode.InnerText);
                if (dateOfArrival <= new DateTime(year, 12, 31))
                {
                    XmlNode dateOfDepartureNode = record.SelectSingleNode("dateOfDeparture");
                    if (dateOfDepartureNode != null && !string.IsNullOrEmpty(dateOfDepartureNode.InnerText))
                    {
                        DateTime dateOfDeparture;
                        if (DateTime.TryParse(dateOfDepartureNode.InnerText, out dateOfDeparture))
                        {
                            if (dateOfDeparture >= new DateTime(year, 12, 31))
                            {
                                filteredRecords.Add(record);
                                count++;
                            }
                        }
                    }
                    else
                    {
                        filteredRecords.Add(record);
                        count++;
                    }
                }
            }

            // Sort the filtered client records based on the specified property
            if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(sortOrder))
            {
                string sortExpression = string.Format("{0} {1}", sortBy, sortOrder);
                filteredRecords.Sort(new XmlNodeComparer(sortExpression));
            }

            // Create a new XML document to hold the sorted and filtered client records
            XmlDocument sortedXmlDoc = new XmlDocument();
            sortedXmlDoc.LoadXml("<result></result>");
            XmlElement sortedRoot = sortedXmlDoc.DocumentElement;

            foreach (XmlNode node in filteredRecords)
            {
                XmlNode importedNode = sortedXmlDoc.ImportNode(node, true);
                sortedRoot.AppendChild(importedNode);
            }

            // Add the count node to the result document
            XmlNode countNode = sortedXmlDoc.CreateElement("Count");
            countNode.InnerText = count.ToString();
            sortedRoot.InsertBefore(countNode, sortedRoot.FirstChild);

            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/stats"), "Stats_End_Year" + "_" + sortBy + "_" + sortOrder + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
            sortedXmlDoc.Save(filePath);
            // Set the sorted and filtered client records as the result
            return sortedXmlDoc;
        }

       


        //IIS Server v nastaveniac
        //členské metódy, členské premenné, parametre čo robia a potom kľúčové metódy, opísať čo prijíma, čo vracia klientovi... potom písať o klientovi, potom ajaks spomenut
        //kľúčové metódy z klienta, nejaký výstup, čo vkladal používateľa čo mu to vrátilo.
        //pdf návod na spustenie a na otvorenie popri dip... potom spomenúť tie log subory v 
    }
}

