// Напишите программу, которая задаёт массив из 8 элементов,
// заполненный псевдослучайными числами и выводит их на экран.
// 1, 2, 5, 7, 19 -> [1, 2, 5, 7, 19]
// 6, 1, 33 -> [6, 1, 33]

Console.WriteLine("Вывод массива из 8-ми элементов, заполненного псевдослучайными цифрами в диапазоне от 0 до 99:");
int[] result = GetArray(8, 99);
PrintArray(result);

// Methods

static int[] GetArray(int size, int max)
{
	if (size == 0)
		return new int[] { };

	if (max < 0)
		max = -max;
	++max;

	Random rnd = new Random();
	int[] array = new int[size];
	for (int i = 0; i < size; ++i)
	{
		array[i] = rnd.Next(0, max);
	}
	return array;
}

static void PrintArray(int[] array)
{
	int lastIndex;
	if (array == null || (lastIndex = array.Length - 1) < 0)
	{
		Console.WriteLine("Массив пуст.");
		return;
	}

	Console.Write("[");
	for (int i = 0; i < lastIndex; i++)
	{
		Console.Write($"{array[i]}, ");
	}
	Console.WriteLine($"{array[lastIndex]}]");
}
