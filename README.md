# Projekt do Szkolenia Technicznego - część warsztatowa

## Wstęp

Utworzenie przy pomocy szablonu "Soneta AddOn (Soneta.SDK)" solution zawierającego typowe projekty dodatku oraz omówienie ich zastosowania: warstwa biznesowa, warstwa UI i warstwa testowa.

Ułatwienia przy tworzeniu i debuggowaniu dodatku. 

## Zad.1

Tworzenie własnegho widoku z danych  programu enova365 - szablon "Soneta - ViewInfo". Rejestracja widoku dla programu enova365.

    KlienciViewInfo.cs
    Klienci.viewform.xml
    ViewInfoReg.cs

## Zad.2

Tworzenie własnego workera operującego na obiektach enova365 - szablon "Soneta - Worker".

Parametry workera. Definiowanie własnego okna parametrów workera.

Rezultaty operacji oraz klasa MessageBoxInformation.

    Kontrahent.UstawRabatWorker.cs
    UstawRabatWorkerParams.Ogolne.pageform.xml

## Zad.3

Rozszerzanie bazy danych enova365 o własne tabele zdefiniowane w dodatku (szablon "Soneta - Business Xml") oraz ich inicjowanie: pliki business.xml i dbinit.xml

    Szkolenie.business.xml (Szkolenie.busines.cs>)
    Szkolenie.dbinit.xml
    Loty.cs + Lot.cs
    Maszyny.cs + Maszyna.cs
    Rezerwacje.cs + Rezerwacja.cs

## Zad.4

Tworzenie widoków dla własnych tabel dodatku.

Definiowanie filtrów dla widoków.

Umożliwienie edycji danych we własnych tabelach (NewRowAttribute).

    KatalogLotow.viewform.xml
    KatalogLotowViewInfo.cs
    KatalogMaszyn.viewform.xml
    KatalogMaszynViewInfo.cs
    Rezerwacje.viewform.xml
    RezerwacjeViewInfo.cs

## Zad.5

Okna edycji danych dla obiektów wprowadzonych przez dodatek (szablon "Soneta - PageForm"). Elementy używane w plikach pageform.xml.

    Lot.ogolne.pageform.xml
    Maszyna.ogolne.pageform.xml
    Rezerwacja.ogolne.pageform.xml

Okna lookupów dla naszych tabel - lookupform.xml

    Lot.lookupform.xml
    Maszyna.lookupform.form

Możliwość nadpisania domyślnych lookupów i dropdownów (GetList).

    Rezerwacja.cs

## Zad.6

Dodanie własnej zakładki na obiekcie z enova365 - extender na podstawie szablonu "Soneta - PageForm".

    KontrahentExtender.cs
    Kontrahent.Loty.pageform.xml
    