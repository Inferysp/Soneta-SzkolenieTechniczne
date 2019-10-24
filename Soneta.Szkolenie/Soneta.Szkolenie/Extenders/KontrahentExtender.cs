// Extender rozszerza klasę standardową i można go później użyć w plikach XML: pageform, viewform i lookupform

using Soneta.Business;
using Soneta.Business.App;
using Soneta.CRM;
using Soneta.Szkolenie.UI;


// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w UI.
[assembly: Worker(typeof(KontrahentExtender))]

namespace Soneta.Szkolenie.UI
{
    public class KontrahentExtender
    {
        [Context]
        public Session Session { get; set; }

        [Context]
        public Login Login { get; set; }

        [Context]
        public Kontrahent Kontrahent { get; set; }

        public View RezerwacjeLotow 
            => SzkolenieModule.GetInstance(Kontrahent).Rezerwacje.WgKlient[Kontrahent].CreateView();

        public bool MaRezerwacje => RezerwacjeLotow.Count > 0;
    }
}
