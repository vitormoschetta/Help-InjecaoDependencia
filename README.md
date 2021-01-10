# Help-InjecaoDependencia

## Exemplo de Controller sem DI:
```
public class CustomerController : Controller
{
    private CustommerRepository _repository = new CustommerRepository();

    public IActionResult Index()
    {
        List<Custommer> custommers = _repository.GetAll();
        return View(custommers);
    }    
}
```

Perceba que no exemplo acima a classe _CustomerController_ instancia a classe _CustommerRepository_.  
Essa prática não é recomendada pelos motivos listados abaixo:  
1. **Alto Acoplamento**: Quando for necessário modificar a implementação da classe _CustommerRepository_, provavelmente precisaremos 
também mexer na classe _CustomerController_, pois esta depende diretamente daquela. Existe o que chamados de Alto Acoplamento.

2. **Testes**: Essa implementação dificulta o desenvolvimento de **Testes de Unidade** por exemplo, uma vez que para efetuar testes a 
nossa estrutura deva ser capaz de receber simulações / mock de clases. A _CustomerController_, da forma que está implementada, exige a 
implementação da classse _CustommerRepository_, e não aceita simulações.
<br>


## Exemplo de Controller com DI:
Antes de tudo, precisamos refletir sobre os dois motivos pelo qual não devemos implementar o modelo anterior (_CustomerController_) discutidos acima:
1. **Alto Acoplamento**
2. **Testes**

Primeiro, nosso controlador não deve instanciar o repositório, mas receber uma instancia deste. Veja:

```
public class ProductController : Controller
{
    private ProductRepository _repository;

    public ProductController(ProductRepository repository) =>
        _repository = repository;


    public IActionResult Index()
    {
        List<Product> products = _repository.GetAll();
        return View(products);
    }
}
```

Veja que o _ProductController_ não instancia _ProductRepository_ como acontece no _CustommerController_. Ao invés de dar um **new** na classe _ProductRepository_, nós a recebemos no _Construtor_ da classe _ProductController_.

Isso, porém ainda não é o suficiente, pois o _Controller_ ainda depende da implementação direta da classe _ProductRepository_. Isso quer
dizer que ainda não podemos fazer simulações, passar dados fictícios para testes, por exemplo.

Um dos princípios SOLID (Inversão de Dependência) diz:
> Módulos de alto nível não devem depender de módulos de baixo nível. Ambos devem depender de abstrações.

Como podemos fazer com que nosso _Controller_ dependa de uma abstração e não da implementação concreta do _Repositorio_?   
R: Interface.

**Interface** é uma abstração, como um contrato que padroniza a implementação de um determinado tipo  / grupo de classes. Veja:
```
public interface IProductRepository
{
    List<Product> GetAll();
    Product Create(Product item);
}
```

Criamos acima uma Interface para o Repositorio de dados que será utilizado pela classe Product. Em si essa interface não faz nada, ela é apenas uma abstração. Agora, vamos implementar essa interface com a classe _ProductRepository_:
```
public class ProductRepository : IProductRepository
{
    List<Product> list;

    public ProductRepository()
    {
        list = new List<Product>() {
            new Product(1, "Product A"),
            new Product(1, "Product B"),
            new Product(1, "Product C"),
        };
    }

    public List<Product> GetAll() => list;

    public Product Create(Product item)
    {
        item.Id = list.Count + 1;
        list.Add(item);
        return item;
    }
}
```

Por fim, Modificar o _ProductController_:

```
public class ProductController : Controller
{
    private IProductRepository _repository;

    public ProductController(IProductRepository repository) =>
        _repository = repository;


    public IActionResult Index()
    {
        List<Product> products = _repository.GetAll();
        return View(products);
    }
}
```

Veja que o _ProductController_ não instancia _ProductRepository_ como acontece no _CustommerController_. Ao invés disso, criamos um 
_Construtor_ para a classe recebendo uma abstração do Repositório, no nosso caso _IProductRepository_.

Agora, Qualquer classe que IMPLEMENTE a Inteface _IproductRepository_ pode ser utilizada no _ProductController_. Ou seja, podemos criar uma classe de repositório fake para efetuar simulações / testes.

Eu posso fazer o seguinte:
```
public class FakeProductRepository : IProductRepository
{ 
    ...
}

var productController = new ProductController(FakeProductRepository)
```

Agora, a pergunta que não quer calar: Em nenhum momento instanciamos um Controlador em nossos projetos, como eles são instanciados e como
sabem qual implementação concreta utilizar?

R: Cada Framework possui sua forma de implementar Injeção de Dependência, e o .NET Core nos disponibiliza isso nativamente, bastando adicionar o seguinte código na classe _Startup_:
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IProductRepository, ProductRepository>();
}
```

No código acima informamos ao serviço de injeção de dependencia do .NET Core que todas as chamadas para a Interface _IProductRepository_ deve-se utilizar a implementação concreta _ProductRepository_.
<br>

###### Referência:
> <https://docs.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection>
<br>
<br>


# Tempo de Vida da Instancia em DI

Quando configuramos a injeção de dependência na classe _Startup_, como fizemos no exemplo anterior, podemos configurar o tempo de vida útil
de cada tipo de instância. No exemplo anterior informamos _AddSingleton_. Existem ainda outros dois tipos no .NET Core, conforme segue abaixo:

1. Transient
2. Scoped
3. Singleton

## Transient
Faz com que seja realizada uma nova instancia para cada Controller / Serviço que implementa a classe. 

## Scoped
Informa ao DI do NET Core que para cada request / requisição deve-se usar uma única instância da classe, independente de quantos Controllers / Serviços a utilizam.

## Singleton
Esse tipo de DI mantém uma única instância para todas as requisições.

Você pode executar o projeto e verificar a diferença:  
<https://localhost:5001/operations> 
<br>
<br>


###### Referências:
> <https://docs.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection-usage>
> <https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences>
