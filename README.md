---
author: Lektion 6
---

# Teams lektion 6

Hej och välkommen!

## Agenda

1. Svar på frågor
2. Repetition
3. Genomgång av övningar
4. Redovisning av övningar
5. Fortsättning på skolsystem
6. Övningar

---

# Fråga

Kan du gå igenom `GetStats`?

# Svar

Den verkar vara gammal, så använd `ping`. Jag visar.

---

# Fråga

Kan du gå igenom grundligt hur man hanterar en databas?

# Svar

Javisst.

---

# Fråga

Kan du gå igenom databas övningarna?

# Svar

Javisst.

---

# Fråga

Ska man dela upp gruppuppgiften i två olika projekt/repositories?

# Svar

Ja, det blir enklast att hantera dem om de är separata.

---

# Fråga

Kan du skriva en lista på hur sockets fungerar och hur de kan användas?

Exempel:

1. Importera namespaces
2. Skapa ip endpoint och socket
3. Anroppa `bind` och `listen`

# Svar

För båda:

1. Importera `System.Net` och `System.Net.Sockets`
2. Lägg upp en ip address och ip endpoint:

```c#
IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
IPEndPoint ipEndpoint = new IPEndPoint(ipAddress, 25000);
```

3. Skapa en socket:

```c#
Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
```

För socket server:

1. Bind och börja lyssna efter anslutningar:

```c#
serverSocket.Bind(ipEndpoint);
serverSocket.Listen();
```

2. Acceptera anslutningar:

```c#
Socket clientSocket = serverSocket.Accept();
```

3. Börja läsa och skicka meddelanden

För socket client:

1. Anslut till server:

```c#
clientSocket.Connect(ipEndpoint);
```

2. Börja läsa och skicka meddelanden

---

# Fråga

Varje gång man startar ett nytt projekt, måste man:

1. Skapa en ny databas, exempelvis `my-mongo2`?
2. Gå in i docker och trycka `run`?
3. Stänga av docker när man avslutar för dagen?

# Svar

1. Nej, man kan återanvända samma databas
   - Jag rekommenderar dock att köra på en ny databas för varje projekt för att separera dem.
2. Ja, om databasen har stoppats
3. Ja, om du inte vill ha på databasen när du inte jobbar med den
