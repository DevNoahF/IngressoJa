using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngressoJa.Contexts.Eventos.Domain.Entities.ValueObject
{
    public class CpfVO
    {
        public String Value { get; private set; }

        public CpfVO(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("CPF must not be empty.");
            
            if (value.Length != 11)
                throw new Exception("CPF must have 11 digits.");
            
            if(value.Contains(".,-,_,@,#,$,%,&,*,(,),+"))
                throw new Exception("CPF must not contain special characters!");

            Value = value;
        }
    }
}