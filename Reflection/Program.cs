using System.Reflection;

namespace ReflectionEx
{
    internal class Program
    {
        static List<PropertyInfo> studentPublicProperties = null;

        static void Main(string[] args)
        {
            DisplayPublicProperties();
            CreateInstance();
        }

        static void DisplayPublicProperties()
        {
            studentPublicProperties = typeof(Student)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            Console.WriteLine("Propriedades públicas da classe Student:\n");

            foreach (var property in studentPublicProperties.Select(x => $"{x.Name} ({x.PropertyType})"))
                Console.WriteLine(property);
        }

        static void CreateInstance()
        {
            var student = (Student)Activator.CreateInstance(typeof(Student));

            studentPublicProperties.Where(x => x.Name == "Name").First().SetValue(student, "Daniel");
            studentPublicProperties.Where(x => x.Name == "University").First().SetValue(student, "UFABC");
            studentPublicProperties.Where(x => x.Name == "RollNumber").First().SetValue(student, 123456);

            Console.WriteLine();
            typeof(Student).GetMethods().Where(x => x.Name == "DisplayInfo").First().Invoke(student, null);
        }
    }
}