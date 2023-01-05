namespace VehicleRental
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=rentals;Trusted_Connection=True;";
            RentalManager manager = new RentalManager(connectionString);

            Console.WriteLine("Benvenuto nell'applicazione per il noleggio di veicoli!");

            while (true)
            {
                Console.WriteLine("Scegli un'opzione:");
                Console.WriteLine("1) Visualizza noleggi");
                Console.WriteLine("2) Visualizza dettagli di un noleggio");
                Console.WriteLine("3) Aggiungi un nuovo noleggio");
                Console.WriteLine("4) Calcola il totale dei noleggi di un cliente");
                Console.WriteLine("5) Esci");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Inserisci id:");
                    int input = int.Parse(Console.ReadLine());
                    Rental rental = manager.GetRentals(input);

                    Console.WriteLine($"ID: {rental.ID} - Veicolo: {rental.Vehicle.Plate} - Cliente: {rental.Customer.FirstName} {rental.Customer.LastName} - Data inizio: {rental.StartDate:yyyy-MM-dd} - Numero giorni: {rental.NumberOfDays} - Costo: {rental.Cost:C}");
                
                 if (input == 0)
                    {
                        Console.WriteLine("Input non valido.");
                    }
                }
                if (option == "2")
                {
                    Console.WriteLine("Inserisci l'ID del noleggio:");
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int rentalID))
                    {
                        Rental rental = manager.GetRentals(rentalID);
                        if (rental != null)
                        {
                            Console.WriteLine($"ID: {rental.ID} - Veicolo: {rental.Vehicle.Plate} - Modello: {rental.Vehicle.Model} - Tariffa giornaliera: {rental.Vehicle.DailyRate:C} - Cliente: {rental.Customer.FirstName} {rental.Customer.LastName} - Codice fiscale: {rental.Customer.ID} - Data inizio: {rental.StartDate:yyyy-MM-dd} - Numero giorni: {rental.NumberOfDays} - Costo: {rental.Cost:C}");
                        }
                        else
                        {
                            Console.WriteLine("Noleggio non trovato.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Input non valido.");
                    }
                }
                if (option == "3")
                {
                    Console.WriteLine("Inserisci la data di inizio del noleggio (formato yyyy-MM-dd):");
                    string startDateInput = Console.ReadLine();
                    if (DateTime.TryParse(startDateInput, out DateTime startDate))
                    {
                        Console.WriteLine("Inserisci il numero di giorni del noleggio:");
                        string numberOfDaysInput = Console.ReadLine();
                        if (int.TryParse(numberOfDaysInput, out int numberOfDays))
                        {
                            Console.WriteLine("Inserisci il codice fiscale del cliente:");
                            string idInput = Console.ReadLine();
                            if (idInput.All(char.IsDigit))
                            {
                                Console.WriteLine("Inserisci la targa del veicolo:");
                                string plateInput = Console.ReadLine();
                                if (plateInput.All(char.IsLetter))
                                {
                                    manager.AddRental(startDate, numberOfDays, idInput, plateInput);
                                    Console.WriteLine("Noleggio aggiunto con successo.");
                                }
                                else
                                {
                                    Console.WriteLine("Targa del veicolo non valida.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Codice fiscale del cliente non valido.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Numero di giorni del noleggio non valido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Data di inizio del noleggio non valida.");
                    }
                }
                if (option == "4")
                {
                    Console.WriteLine("Inserisci il codice fiscale del cliente:");
                    string idInput = Console.ReadLine();
                    if (idInput.All(char.IsDigit))
                    {
                        decimal totalCost = manager.GetTotalRentalCost(idInput);
                        Console.WriteLine($"Il totale dei noleggi del cliente è di {totalCost:C}.");
                    }
                    else
                    {
                        Console.WriteLine("Codice fiscale del cliente non valido.");
                    }
                    break;
                }
                if (option == "5")
                {
                    Console.WriteLine("Arrivederci!");
                    return;
                }
            }
        }
    }
}