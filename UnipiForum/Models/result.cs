//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnipiForum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class result
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int test_id { get; set; }
        public int question_id { get; set; }
        public int answer_id { get; set; }
        public Nullable<bool> user_answer { get; set; }
        public string question_text { get; set; }
        public string answer_text { get; set; }
        public string test_text { get; set; }
        public Nullable<bool> is_the_correct_answer { get; set; }
    }
}
