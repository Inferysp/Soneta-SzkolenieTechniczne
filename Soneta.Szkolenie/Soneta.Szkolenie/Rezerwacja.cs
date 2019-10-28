﻿using Soneta.Business;
using Soneta.CRM;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:NewRow(typeof(Soneta.Szkolenie.Rezerwacja))]

namespace Soneta.Szkolenie
{
    public class Rezerwacja : SzkolenieModule.RezerwacjaRow
    {
        public object GetListCzyOplacona()  // Funkcja definiująca zawartość dropdowna lub lookupa wyswietlanego dla pola.
                                            // Jej nazwa musi zaczynać się od "GetList", a następnie zawierać nazwę edytowanego property
        {
            return new[] { 
                CzyOplacone.Nieoplacone, 
                CzyOplacone.Oplacone 
            };  // z enuma wydzielamy do wyświetlenia tylko to, co nas interesuje
                // dzięki temu "Razem" nie pojawi się w dropdownie
        }

        protected override void OnAdded() 
        {
            base.OnAdded();
            this.CzyOplacona = CzyOplacone.Nieoplacone;
        }

        [AttributeInheritance]
        public new Lot Lot
        {
            get => base.Lot;
            set
            {
                base.Lot = value;

                var poRabacie = Percent.Hundred;
                if (Klient != null)
                    poRabacie -= Klient.Rabat;

                CenaLotu = Lot.Cena * poRabacie;
            }
        }

        [AttributeInheritance]
        [Description("Cena za lot po uwzględnieniu rabatu")]
        public new Currency CenaLotu { get; set; }
    }
}
