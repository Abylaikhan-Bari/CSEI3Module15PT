using System;
using System.Reflection;

public class MyClass
{
    private int privateField = 42;

    public MyClass() { }

    public MyClass(int value)
    {
        privateField = value;
    }

    public int PublicProperty { get; set; }

    private void PrivateMethod()
    {
        Console.WriteLine("PrivateMethod() called.");
    }

    public void PublicMethod()
    {
        Console.WriteLine("PublicMethod() called.");
    }
}

class Program
{
    static void Main()
    {
        Type myClassType = typeof(MyClass);
        object instance = Activator.CreateInstance(myClassType);

        // Extra Challenge: Mini-Interpreter
        Console.WriteLine("\nEnter 'exit' to quit the interpreter.");
        while (true)
        {
            Console.Write("Enter method name and parameters (e.g., PublicMethod or PrivateMethod): ");
            string input = Console.ReadLine().Trim();

            if (input.ToLower() == "exit")
            {
                break; // Exit the interpreter loop
            }

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string methodName = parts[0];
            string methodParameters = parts.Length > 1 ? parts[1] : null;

            MethodInfo methodInfo = myClassType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (methodInfo != null)
            {
                try
                {
                    object result = methodInfo.Invoke(instance, methodParameters != null ? new object[] { methodParameters } : null);
                    Console.WriteLine($"Method '{methodName}' executed successfully.");
                    if (result != null)
                    {
                        Console.WriteLine($"Result: {result}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing method '{methodName}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Method '{methodName}' not found.");
            }
        }

        Console.ReadKey();
    }
}
