using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5058");
        var client = channel.CreateGrpcService<IStudentService>();

        var reply = await client.GetAllProfilesAsync(new Empty());

        if(reply.Students != null)
        {
            foreach(var student in reply.Students!) {
                Console.WriteLine(student.Id);
                Console.WriteLine(student.FullName);
                Console.WriteLine(student.Birthday);
                Console.WriteLine(student.Address);
                Console.WriteLine(student.ClassId);
                Console.WriteLine(student.ClassName);
            }
        }
        if(reply.Message != null)
        {
            Console.WriteLine(reply.Message);
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}