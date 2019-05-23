# Participação nos Lucros

1 - Running this Solution

    1.1 - Build and run the Dockerfile in cmd or terminal.
        docker build -t participacao .
        docker run -p 7071:80 --name participacao participacao 
        Go on your browser to http://localhost:7071/ it should works.

    1.2 - Run AzureFunction project through Visual Studio 2019.
        Open the solution file ParticipacaoLucros.sln.
        Requirements .NetCore 2.1 and Microsoft Azure Storage Running.
        Run the solution.
        Go on your browser to http://localhost:7071/ it should works.

2 - Testing this Solution

    2.1 - Run Tests projects through Visual Studio 2019.
        Open the solution file ParticipacaoLucros.sln.
        Build it. In the Test Explorer Windows, both Integration and Unit Tests should appear.
        If you have a running solution, all the tests should pass.

3 - Database Interaction

    3.1 - Currently I am only saving the last result received by 
    sending a Post Request to the method Funcionarios. 

4 - Current Methods:
    Funcionarios - Post or Put. Funcionarios/ To register new Funcionarios over the solution
    Funcionarios - Get. Funcionarios/ To see the current list of Funcionarios
    Funcionarios_CalculaLucros - Get. Funcionarios/CalculaLucros/{total_disponibilizado} To run the
    CalculaLucros and get the RetornoLucros model as return.