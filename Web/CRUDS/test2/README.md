## O que e isso ?

bom, isso e um site que voce se cadastra para nada, isso mesmo, para nada, aqui eu fiz isso so pra ver como salvar dados no banco de dados.

## O que era pra ser isso ?

era pra ser um forum, mas eu descobrir que tem que fazer alguma forma de autenticação para o usuario pode entra, so que eu não sei fazer isso, e voce deve estar se perguntando "e por que voce nao pesquisa ?", pois bem, eu acho que tenho que ler mais como funciona a internet, e outra coisa atiçou minha curiosidade!, que foi o blazor, eu nem sei pra que isso serve direito...

Mesmo assim, isso me inpede de que no futuro eu faça um forum, e so que eu to com priguiça de pesquisar sobre isso agora, e eu acho que esse projeto ja estar no fim da linha...

## O que te atrapalhou pra chegar aqui ?

bom, a minha burrice, porque, simples, o test2Context precisa receber algum para salvar dados no banco de dados, que e a conexão(eu acho, ainda nao entendi o que ele precisa direito!), bom oque o ajete faz(pelo menos os iniciates!), vão no Controller e criam um construtor que passa essa informação para o banco de dados, mas como minha burrice e grande, eu tava fazendo alcontrario, ou seja eu tava fazendo com que o paremetro recebe-se o banco de dados (context=_context), so que o banco de dados que recebe o paremetro (_context=context), e isso tava me atrapalhando, pois ele dava que nao recebia nada quando eu enviava os dados!