using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Management
{
    public abstract class SubModulesDTO
    {
        public Logoempresa[] logoempresa { get; set; }
        public Changepassword[] changepassword { get; set; }
        public Thirdparty[] thirdparties { get; set; }
        public Typeslistbyquery[] typeslistbyquery { get; set; }
        public Procurator[] procurator { get; set; }
        public User[] users { get; set; }
        public Access[] access{ get; set; }
        public Setting[] setting { get; set; }
        public Sentto[] sentto { get; set; }
        public Monitoring[] monitoring { get; set; }
        public Individual[] individual { get; set; }
        public Massive[] massive { get; set; }
        public Type[] type { get; set; }
        public Manager[] manager { get; set; }
        public Historyreport[] historyreport { get; set; }
        public Getlog[] getlog { get; set; }
        public Queriesandmatch[] queriesandmatches { get; set; }
        public Coincidencedetailing[] coincidencedetailing { get; set; }
        public Viewpastconsultation[] viewpastconsultations { get; set; }
        public Certificationlistupdate[] certificationlistupdates { get; set; }
        public Parameterscategory[] parameterscategory { get; set; }
        public News[] news { get; set; }
        public Roi[] roi { get; set; }
        public Faq[] faqs { get; set; }
        public Signal[] signal { get; set; }
    }

    public class Logoempresa
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Changepassword
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Thirdparty
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Typeslistbyquery
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Procurator
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class User
    {
        public bool status { get; set; }
        public int id { get; set; }
    }
    public class Access
    {
        public bool status { get; set; }
        public int id { get; set; }
    }


    public class Setting
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Sentto
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Monitoring
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Individual
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Massive
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Type
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Manager
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Historyreport
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Getlog
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Queriesandmatch
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Coincidencedetailing
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Viewpastconsultation
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Certificationlistupdate
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Parameterscategory
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class News
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Roi
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Faq
    {
        public bool status { get; set; }
        public int id { get; set; }
    }

    public class Signal
    {
        public bool status { get; set; }
        public int id { get; set; }
    }


}
