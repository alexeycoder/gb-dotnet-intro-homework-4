// Задача 25: Напишите цикл, который принимает на вход два числа (A и B) и возводит число A в натуральную степень B.
// 3, 5 -> 243 (3⁵)
// 2, 4 -> 16

do
{
	Console.Clear();
	PrintTitle("Возведение числа в натуральную степень", ConsoleColor.Cyan);

	decimal number = ReadAnyDouble("Введите число, которое необходимо возвести в степень: ");
	int exp = ReadNatural("Введите натуральный показатель степени: ");

	decimal power = GetPower(number, exp);
	string expStr = ToSuperscriptedNumber(exp);

	PrintColored($"{number}{expStr} = {power}", ConsoleColor.Yellow);

} while (AskForRepeat());

// Methods

static decimal GetPower(decimal baseNum, int exponent)
{
	if (baseNum == 1)
		return 1;

	if (baseNum == 0)
		if (exponent != 0)
			return 0;
		else  // для универсальности; не должно случиться для натуральной степени
			throw new ArgumentOutOfRangeException(nameof(exponent));

	if (exponent == 0) // для универсальности; не должно случиться для натуральной степени
		return 1;

	decimal result = baseNum;

	// один из вариантов оптимизации: две стадии в целях уменьшения
	// числа итераций для больших показателей степеней

	// стадия 1: возведение в степень, кратную степеням двойки
	int e = 2;
	while (e <= exponent)
	{
		result *= result;
		e *= 2;
	}

	// стадия 2: "до-возведение" до заданной степени
	e /= 2;
	while (++e <= exponent)
	{
		result *= baseNum;
	}

	return result;
}

static string ToSuperscriptedNumber(int num)
{
	bool negative = num < 0;
	if (negative)
		num = -num;

	string result = string.Empty;
	for (; num > 0; num /= 10)
	{
		int digit = num % 10;
		result = ToSuperscriptedDigit(digit) + result;
	}

	if (negative)
		result = "-" + result;

	return result;
}

static char ToSuperscriptedDigit(int digit)
{
	switch (digit)
	{
		case 1:
			return '\u00b9';
		case 2:
			return '\u00b2';
		case 3:
			return '\u00b3';
		case 4:
			return '\u2074';
		case 5:
			return '\u2075';
		case 6:
			return '\u2076';
		case 7:
			return '\u2077';
		case 8:
			return '\u2078';
		case 9:
			return '\u2079';
		case 0:
		default:
			return '\u2070';
	}
}

const string errorMsg = "Некорректный ввод! Пожалуйста повторите";

static decimal ReadAnyDouble(string message)
{
	return GetUserInput(message, errorMsg);
}

static int ReadNatural(string message)
{
	return (int)GetUserInput(message, errorMsg, d => d > 0 && d == Math.Floor(d));
}

static decimal GetUserInput(string inputMessage, string errorMessage, Func<decimal, bool>? checkIfValid = null)
{
	decimal input = 0;
	bool handleError = false;
	do
	{
		if (handleError)
		{
			PrintError(errorMessage, ConsoleColor.Magenta);
		}
		Console.Write(inputMessage);

		string? inputStr = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(inputStr))
		{
			handleError = true;
			continue;
		}
		handleError = !(decimal.TryParse(MakeInvariantToSeparator(inputStr), out input)
					&& (checkIfValid == null || checkIfValid(input)));

	} while (handleError);

	return input;
}

static string MakeInvariantToSeparator(string input)
{
	char decimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
	char wrongSeparator = decimalSeparator.Equals('.') ? ',' : '.';
	return input.Replace(wrongSeparator, decimalSeparator);
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