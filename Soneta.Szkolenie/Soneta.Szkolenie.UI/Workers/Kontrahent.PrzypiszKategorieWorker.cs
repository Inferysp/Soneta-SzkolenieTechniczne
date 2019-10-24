using System;
using System.Collections.Generic;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Szkolenie.UI;
using Soneta.Tools;
using Soneta.Types;

[assembly: Worker(typeof(PrzypiszKategorieWorker), typeof(Kontrahenci))]

namespace Soneta.Szkolenie.UI
{
    public class PrzypiszKategorieWorker: ContextBase
    {
        public PrzypiszKategorieWorker(Context ctx) : base(ctx)
        {
        }

        [Context]
        public Kontrahent[] WybraniKontrahenci { get; set; }

        private List<Kontrahent> doZmiany; 

        [Context]
        public PrzypiszKategorieWorkerParams @params
        {
            get;
            set;
        }

        [Action("Przypisz kategorię", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public MessageBoxInformation PrzypiszKategorie()
        {

            if (WybraniKontrahenci.Length == 0)
                return new MessageBoxInformation("Błąd!", "Nie wybrano żadnego kontrahenta.") { OKHandler = null };

            doZmiany = new List<Kontrahent>();

            foreach (var kth in WybraniKontrahenci)
                if (kth.Kategorie.Contains( @params.Kategoria))
                {
                    doZmiany.Add(kth);
                }

            if (doZmiany.Count == 0)
                return new MessageBoxInformation("Błąd!", "W wybranych zadaniach nie ma nic do zrobienia.") { OKHandler = null };

            return new MessageBoxInformation("Przypisanie kontrahenta")
            {
                Text = "Czy przypisać wybranych kontrahentów ({0}) do kategorii ({1})?"
                    .TranslateFormat( doZmiany.Count, @params.Kategoria),
                YesHandler = PrzypiszKtg,
                NoHandler = () => null
            };
        }

        private object PrzypiszKtg()
        {
            //var tm = CRMModule.GetInstance(Session);
            //using (var tr = Session.Logout(true))
            //{
            //    foreach (var kth in doZmiany)
            //        kth.Kategorie = @params.Kategoria;
            //    tr.Commit();
            //}
            return "Operacja została zakończona.";
        }
    }

    public class PrzypiszKategorieWorkerParams : ContextBase
    {
        public PrzypiszKategorieWorkerParams(Context context) : base(context)
        {
        }

        [Caption("Kategoria do przypisania")]
        public KategoriaKth Kategoria { get; set; }
    }
}
