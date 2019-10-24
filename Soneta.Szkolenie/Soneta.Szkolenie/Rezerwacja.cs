using Soneta.Business;
using System;
using System.Collections.Generic;
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
    }
}
