using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountMeIn.Core.Model
{
    public class Group_Menber
    {
        public int Group_Member_Id { get; set; }
        public string Group_Name { get; set; }
        public List<Member> Members { get; set; }
    }
}
