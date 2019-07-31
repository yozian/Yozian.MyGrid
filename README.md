# Features

* Easy to render table with strong typed

![image](https://raw.githubusercontent.com/yozian/Yozian.MyGrid/master/resources/example-result.png)

# Render Example

```csharp

    // model
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Book Name")]
        public string Name { get; set; }

        [Display(Name = "Create Time")]
        public DateTime CreatedAt { get; set; }
    }

```

``` csharp

     @*razor view*@
    <div class="text-center">
        @{
            
            var table = MyGrid.NewGrid(Model)
                .Id("my-table")
                .HtmlAttributes(new
                {
                    width = "400px;"
                })
                .DefineColumns((builder) =>
                {
                    // exmaple for get the display name default to property name
                    builder.ColumnFor(m => m.Id);

                    // exmaple for get the display name as header title
                    builder.ColumnFor(m => m.Name);

                    // exmaple for apply a formatter
                    builder.ColumnFor(m => m.CreatedAt).Format(MyFormatter.DateTime);

                    // exmaple for customized cell contents
                    builder.ColumnFor(
                        "Operation",
                        (td, m) =>
                        {
                            var btn = new TagBuilder("button");
                            btn.InnerHtml.Append("Edit");

                            td.InnerHtml.AppendHtml(btn);
                        }
                    );
                })
                .Render();

        }
        
        @*output*@
        @Html.Raw(table)
    </div> 


```


``` csharp
    
    // signature for Grid Format method
    public static class MyFormatter
    {
        public static string DateTime<T>(object value, T model)
        {
            return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }

```


# Generated Html

``` html

<table class="" id="my-table" width="400px;">
    <thead>
        <tr>
            <th class="all">Id</th>
            <th class="all">Book Name</th>
            <th class="all">Create Time</th>
            <th class="all">Operation</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td>
            <td>Advanced C# Programing</td>
            <td>2019-01-02 00:00:00</td>
            <td>
                <button>Edit</button>
            </td>
        </tr>
        <tr>
            <td>1</td>
            <td>Javascript Programing</td>
            <td>2019-07-31 00:00:00</td>
            <td>
                <button>Edit</button>
            </td>
        </tr>
    </tbody>
</table>

```

