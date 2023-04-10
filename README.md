# Desafio Desenvolvimento - Auvo Sistemas

Este desafio foi feito para o teste de Desenvolvedor .Net na empresa Auvo.

---

### Especificações técnicas
- ASP.Net MVC
- .Net Core 6
- DDD

### Especificações do Sistema
Dois projetos foram realizados, um chamado de Apresentação e o outro chamado de Pagamentos. O projeto Apresentação foi desenvolvido com ASP.Net MVC e possui duas visualizações:

- Home: Exibe informações do candidato, como nome, vaga, pretensão salarial e data do desafio.
- About: Contém um texto que descreve qual desafio foi o mais significativo na jornada do candidato até o momento.

Já o projeto Pagamentos é um console application em .Net6 que utiliza a abordagem DDD para divisão de camadas. Ele recebe um caminho para arquivos de folhas de ponto e, em seguida, processa esses arquivos para gerar um arquivo json contendo todas as ordens de pagamento de cada departamento e funcionário.

---
### Forma de executar
Para rodar os projetos, abra o Visual Studio e selecione:
- "WebApp" para executar o projeto Apresentação.
- "ConsoleApp" para executar o projeto Pagamentos.
