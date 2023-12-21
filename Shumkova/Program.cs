using Microsoft.EntityFrameworkCore;
using Shumkova.Models;
using (VelvetEyebrowsShumkovaContext context = new())
{
   
    //1 запрос

     foreach (Service service in context.Services.Include(c => c.ServicePhotos))
     {
         Console.WriteLine($"{service.Id} {service.Title} {service.Cost} {service.DurationInSeconds} {service.Description} {service.Discount} {service.MainImagePath}");
     }

    //2 запрос

    foreach (Service service in context.Services.Include(c => c.ServicePhotos))
    {
        int min = service.DurationInSeconds / 60;
        int sec = service.DurationInSeconds - min * 60;
        Console.WriteLine($"\n{service.Title}\n{service.Cost}руб.\nДлительность:{min}мин {sec}сек");
    }

    //3 запрос

    foreach (Service service in context.Services.OrderByDescending(c => c.Cost).Take(5))
        {
        Console.WriteLine($"\n{service.Title}\n {service.Cost} ");
    }

    //4 запрос 

    foreach (Service service in context.Services.Include(c => c.ServicePhotos).Where(c => c.Discount > 0))
    {
        // double endCost = service.Cost - service.Cost * (service.Discount / 100);
        var n = Convert.ToDouble(service.Discount);
        var m = Convert.ToDouble(service.Cost);
        double disc = m - n * m;
        Console.WriteLine($"\n{service.Title} {service.Cost}\nЦена со скидкой: {disc} ");
    }

    //5 запрос

    foreach (Client client in context.Clients.Include(c => c.ClientServices))
    {
        Console.WriteLine($"{client.FirstName} {client.LastName} {client.Patronymic} {client.Birthday} {client.GenderCode}");
    }

    //6 запрос

    foreach (ClientService client in context.ClientServices.Include(c => c.Client).Include(x => x.Service).Where(d => d.Id==39))
    {
        Console.WriteLine($"{client.Client.FirstName} {client.Client.LastName}\nУслуга: {client.Service.Title}");
    }

    //7 запрос

    foreach (ClientService client in context.ClientServices.Include(c => c.Client).Include(x => x.Service))
    {
        Console.WriteLine($"\n{client.Client.LastName}\nУслуга: {client.Service.Title}\nВремя начала:{client.StartTime}");
    }

    //доп1

    foreach (Client client in context.Clients.Include(c => c.ClientServices))
    {
        Console.WriteLine($"\n{client.LastName}\nКоличество услуг: {client.ClientServices.Count}");
    }

    //доп2

    foreach (Service service in context.Services.Include(c => c.ClientServices))
    {
        Console.WriteLine($"\n{service.Title}\n {service.ClientServices.Count*service.Cost}");
    }
}
