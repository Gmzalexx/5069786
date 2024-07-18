using _5069786.Models;
using _5069786.Data;
using System.Collections.ObjectModel;

namespace _5069786.Views;

public partial class TodoListPage : ContentPage
{

    TodoItemDatabase database;
    public ObservableCollection<TodoItem> Items { get; set; } = new();

    public TodoListPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
        BindingContext = this;
    }


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            items.Clear();
            foreach (var item in items)
                items.Add(item);
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TodoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new TodoItem()
        });


    }
} 