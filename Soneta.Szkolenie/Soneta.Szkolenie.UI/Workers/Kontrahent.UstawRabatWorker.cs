using System;
using System.Collections.Generic;
using System.ComponentModel;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Szkolenie.UI;
using Soneta.Tools;
using Soneta.Types;

[assembly: Worker(typeof(UstawRabatWorker), typeof(Kontrahenci))]

namespace Soneta.Szkolenie.UI
{
    public class UstawRabatWorker: ContextBase
    {
        public UstawRabatWorker(Context ctx) : base(ctx)
        {
        }

        [Context]
        public Kontrahent[] WybraniKontrahenci { get; set; }

        private List<Kontrahent> doZmiany; 

        [Context]
        public UstawRabatWorkerParams @params
        {
            get;
            set;
        }

        [Action("Loty widokowe/Ustaw rabat", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public MessageBoxInformation UstawRabatTowaru()
        {

            if (WybraniKontrahenci.Length == 0)
                return new MessageBoxInformation("Błąd!", "Nie wybrano żadnego kontrahenta.") { OKHandler = null };

            doZmiany = new List<Kontrahent>();

            foreach (var kth in WybraniKontrahenci)
            {
                var nowyRabat = @params.DodawacRabaty ? kth.RabatTowaru + @params.Rabat : @params.Rabat;

                if (!@params.ObnizacRabaty && nowyRabat < kth.Rabat)
                    continue;

                doZmiany.Add(kth);
            }

            if (doZmiany.Count == 0)
                return new MessageBoxInformation("Błąd!", "Dla wybranych kontrahentów nie ma nic do zrobienia.") { OKHandler = null };

            return new MessageBoxInformation("Ustawienie rabatu")
            {
                Text = "Czy ustawić rabat ({0}) wybranym kontrahentom ({1})?"
                    .TranslateFormat( doZmiany.Count, @params.Rabat),
                YesHandler = UstawRabat,
                NoHandler = () => null
            };
        }

        private object UstawRabat()
        {
            using (var tr = Session.Logout(true))
            {
                foreach (var kth in doZmiany)
                    if (!@params.ObnizacRabaty && @params.Rabat < kth.Rabat)
                        kth.RabatTowaru = @params.Rabat;
                tr.Commit();
            }
            return "Operacja została zakończona.";
        }
    }

    public class UstawRabatWorkerParams : ContextBase
    {
        public UstawRabatWorkerParams(Context context) : base(context)
        {
        }

        [Caption("Wysokość rabatu")]
        public Percent Rabat { get; set; }

        public bool _dodawacRabaty = false;
        [Caption("Czy dodawać rabaty")]
        [Description("Czy dodawać rabaty do siebie?")]
        public bool DodawacRabaty
        {
            get => _dodawacRabaty;
            set
            {
                _dodawacRabaty = value;
                if (_dodawacRabaty)
                    ObnizacRabaty = false;

                OnChanged(EventArgs.Empty);
            }
        }
        [Caption("Czy obniżać rabaty")]
        [Description("Czy zmniejszyć przypisany rabat jeśli już przypisany jest wyższy?")]
        public bool ObnizacRabaty { get; set; } = false;

        public bool IsReadOnlyObnizacRabaty() => DodawacRabaty;
    }
}
