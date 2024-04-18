using System;
using System.Collections.Generic;

public class ChildSearchModel
{
    public int userID { get; set; }
    public int id { get; set; }
    public string search{get;set;}
    public string name{get;set;}
    public string surname{get;set;}
    public string schoolname{get;set;}
    public int registrationnumber{get;set;}

    public string healthstate{get;set;}
    public string comments{get;set;}
    public int postalcode{get;set;}


    public DateTime birthdate{get;set;}
    public string email{get;set;}
    public int phone{get;set;}
    public bool conditions_together { get; set; }

    List<List<string>> info{get;set;}




}