// Задача 27: Напишите программу, которая принимает на вход число и выдаёт сумму цифр в числе.
// 452 -> 11
// 82 -> 10
// 9012 -> 12

do
{
	Console.Clear();
	PrintTitle("Подсчёт суммы цифр числа", ConsoleColor.Cyan);

	int number = GetUserInput("Введите целое число: ", "Некорректный ввод! Пожалуйста повторите");

	int digitsSum = SumDigits(number);
	PrintColored($"Cумма цифр числа {number} -> {digitsSum}", ConsoleColor.Yellow);

} while (AskForRepeat());

// Methods

static int SumDigits(int num)
{
	if (num < 0)
		num = -num;

	int result = 0;
	for (; num > 0; num /= 10)
	{
		result += num % 10;
	}

	return result;
}

static int GetUserInput(string inputMessage, string errorMessage)
{
	int input;
	bool handleError = false;
	do
	{
		if (handleError)
		{
			PrintError(errorMessage, ConsoleColor.Magenta);
		}
		Console.Write(inputMessage);
		handleError = !(int.TryParse(Console.ReadLine(), out input));

	} while (handleError);

	return input;
}

static void PrintTitle(string title, ConsoleColor foreColor)
{
	string titleDelimiter = new string('\u2550', title.Length);
	PrintColored(titleDelimiter + Environment.NewLine + title + Environment.NewLine + titleDelimiter, foreColor);
}

static void PrintError(string errorMessage, ConsoleColor foreColor)
{
	PrintColored("\u2757 Ошибка: " + errorMessage, foreColor);
}

static void PrintColored(string message, ConsoleColor foreColor)
{
	var bkpColor = Console.ForegroundColor;
	Console.ForegroundColor = foreColor;
	Console.WriteLine(message);
	Console.ForegroundColor = bkpColor;
}

static bool AskForRepeat()
{
	Console.WriteLine();
	Console.WriteLine("Нажмите Enter, чтобы повторить или Esc, чтобы завершить...");
	ConsoleKeyInfo key = Console.ReadKey(true);
	return key.Key != ConsoleKey.Escape;
}
