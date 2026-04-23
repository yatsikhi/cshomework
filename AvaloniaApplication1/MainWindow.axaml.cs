using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvaloniaApplication1.Context;
using AvaloniaApplication1.InsideClass;
using AvaloniaApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void button_result(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Message.Text))
        {
            GetTable tableGetter = new GetTable();

            string fruitsText = tableGetter.GetFruitsString();

            Message.Text = fruitsText;
        }
        else
        {
            Message.Text = string.Empty;
        }
    }





    private void sorting_box(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            var combobox = (ComboBox)sender;
            if (combobox?.SelectedItem is ComboBoxItem selectedItem)
            {
                try
                {
                    using (var context = new PostgresContext())
                    {
                        IEnumerable<Product> products = context.Products.AsQueryable();

                        if (selectedItem.Tag.ToString() == "high")
                        {
                            products = products.OrderByDescending(p => p.Id);
                        }
                        else
                        {
                            products = products.OrderBy(p => p.Id);
                        }

                        Message.Text = string.Join(Environment.NewLine, products.Select(p => $"{p.Id} - {p.Name}"));
                    }
                }
                catch (Exception exception)
                {
                    Message.Text = $"mistake {exception.Message}";
                }
            }
        }
    }


    private void filter_box(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = sender as ComboBox;
        if (comboBox?.SelectedItem is ComboBoxItem selectedItem)
        {
            try
            {
                using (var context = new PostgresContext())
                {
                    string type = selectedItem.Tag.ToString();

                    var products = context.Products
                        .Where(p => p.Type == type)
                        .OrderBy(p => p.Id)
                        .ToList();

                    Message.Text = products.Any()
                        ? string.Join(Environment.NewLine, products.Select(p => $"{p.Id} - {p.Name}"))
                        : $"data not found '{type}'.";
                }
            }
            catch (Exception exception)
            {
                Message.Text = $"Mistake: {exception.Message}";
            }
        }
    }

    private void Search_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            try
            {
                using PostgresContext context = new PostgresContext();
                string inputProduct = Search.Text.Trim();
                if (!string.IsNullOrEmpty(inputProduct))
                { 
                    var products = context.Products
                        .Where(p => EF.Functions.Like(p.Name, $"%{inputProduct}%"))
                        .ToList();

                    if (products.Any())
                    {
                        Message.Text = string.Join(Environment.NewLine,
                            products.Select(p => $"{p.Id} - {p.Name} ({p.Type})"));
                    }
                    else
                    {
                        Message.Text = $"No products found containing '{inputProduct}'.";
                    }
                }


            }
            catch (Exception exception)
            {
                Message.Text = $"Error: {exception.Message}";
            }
        }
    }

    private void Sign_in(object? sender, RoutedEventArgs e)
    {
        SignInWindow registrationWindow = new SignInWindow();
        registrationWindow.ShowDialog(this); 
    }

    private void Sign_up(object? sender, RoutedEventArgs e)
    {
        SignUpWindow registrationWindow = new SignUpWindow();
        registrationWindow.ShowDialog(this); 
    }
}