using Bank_Zem_Artem;

public class Bank
{
    static Dictionary<int, User> users = new Dictionary<int, User>();
    static User currentUser;
    static User transferUser;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать пользователя");
            Console.WriteLine("2. Переключиться на пользователя по номеру счета");
            Console.WriteLine("0. Выход");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    Switch();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }

        }
    }

    public static void CreateUser()
    {
        Console.WriteLine("Введите номер счета:");
        int number = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите ФИО пользователя:");
        string fio = Console.ReadLine();

        Console.WriteLine("Введите сумму на счету:");
        float sum = float.Parse(Console.ReadLine());

        User user = new User()
        {
            Number = number,
            FIO = fio,
            Sum = sum
        };

        users[number] = user;

        Console.WriteLine("\nПользователь успешно создан.\n");
    }

    public static void Switch()
    {
        Console.WriteLine("Введите номер счета пользователя:");
        int number = Convert.ToInt32(Console.ReadLine());

        if (users.ContainsKey(number))
        {
            currentUser = users[number];
            Console.WriteLine($"Успешно переключено на пользователя с номером счета: {number}\n");
            Console.WriteLine($"Добро пожаловать {currentUser.FIO}!\n");

            while (true)
            {
                Console.WriteLine("" +
                    "Меню пользователя:\n" +
                    "1. Вывести информацию о текущем пользователе\n" +
                    "2. Пополнить счёт\n" +
                    "3. Снять со счёта\n" +
                    "4. Снять все деньги со счёта\n" +
                    "5. Перевести на другой счёт\n" +
                    "0. Вернуться в главное меню\n");


                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice)
                {
                    case 2:
                        Dob();
                        break;
                    case 1:
                        Out();
                        break;
                    case 3:
                        Umen();
                        break;
                    case 4:
                        Obnul();
                        break;
                    case 5:
                        Transfer();
                        break;
                    case 0:
                        return;
                }
            }
        }
        else
        {
            Console.WriteLine($"Пользователь с номером счета {number} не существует");
        }
    }

    public static void Out()
    {
        if (currentUser != null)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Информация о текущем пользователе:");
            Console.WriteLine($"Номер счета: {currentUser.Number}");
            Console.WriteLine($"ФИО пользователя: {currentUser.FIO}");
            Console.WriteLine($"Сумма на счету: {currentUser.Sum}");
            Console.WriteLine("------------------------------");
        }
        else
        {
            Console.WriteLine("Пользователь не выбран.");
        }
    }

    public static void Dob()
    {
        Console.WriteLine($"Сколько хотите положить на счёт? На счету: {currentUser.Sum}руб");
        float Amount = float.Parse(Console.ReadLine());

        currentUser.Sum += Amount;
        Console.WriteLine($"\nНа вашем счету: {currentUser.Sum}руб\n");
    }

    public static void Umen()
    {
        Console.WriteLine($"Сколько хотите снять со счёта? На счету: {currentUser.Sum}руб");
        float Amount = float.Parse(Console.ReadLine());
        float original = currentUser.Sum;

        if (original - Amount > 0)
        {
            currentUser.Sum = original - Amount;
            Console.WriteLine($"\nНа вашем счету: {currentUser.Sum}руб\n");
        }
        if (original - Amount < 0)
        {
            Console.WriteLine("Ошибка. У вас нет денег на эту сумму");
        }
    }

    public static void Obnul()
    {
        Console.WriteLine($"Вы сняли {currentUser.Sum}руб");
        currentUser.Sum -= currentUser.Sum;
        Console.WriteLine($"\nНа вашем счету:  {currentUser.Sum} руб\n");
    }

    public static void Transfer()
    {
        Console.WriteLine("Введите номер счёта для перевода:");
        int accountNumber = Convert.ToInt32(Console.ReadLine());

        if (users.ContainsKey(accountNumber))
        {
            Console.WriteLine("Введите сумму для перевода:");
            float amount = float.Parse(Console.ReadLine());
            if (amount <= currentUser.Sum)
            {
                transferUser = users[accountNumber];
                currentUser.Sum -= amount;
                transferUser.Sum += amount;

                Console.WriteLine("Перевод выполнен успешно.");
                Console.WriteLine($"Сумма {amount}руб переведена на счет пользователя с номером счета {transferUser.Number}");
                Console.WriteLine($"На вашем счету осталось: {currentUser.Sum}руб");
            }
            else
            {
                Console.WriteLine("Ошибка. Недостаточно средств на счету для перевода.");
            }

        }
        else
        {
            Console.WriteLine($"Пользователь с номером счета {accountNumber} не существует");
        }
    }
}

