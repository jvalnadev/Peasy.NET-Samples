﻿using Orders.com.BLL;
using Orders.com.DAL.EF;
using Orders.com.WPF.VM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Orders.com.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerService _customersService;
        private OrderService _ordersService;
        private OrderItemService _orderItemsService;
        private ProductService _productsService;
        private CategoryService _categoriesService;
        private EventAggregator _eventAggregator = new EventAggregator();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _ordersService = new OrderService(new OrderRepository());
            _customersService = new CustomerService(new CustomerRepository());
            _orderItemsService = new OrderItemService(new OrderItemRepository());
            _productsService = new ProductService(new ProductRepository());
            _categoriesService = new CategoryService(new CategoryRepository());
            var ordersService = new OrderService(new OrderRepository());
            this.DataContext = new MainWindowVM(_eventAggregator, _customersService, _productsService, _categoriesService, ordersService);
        }

        public MainWindowVM VM
        {
            get { return this.DataContext as MainWindowVM; }
        }

        private void addCustomerOrderClick(object sender, RoutedEventArgs e)
        {
            var customerOrderWindow = new CustomerOrderWindow(_ordersService, _customersService, _orderItemsService, _productsService, _categoriesService, _eventAggregator);
            var result = customerOrderWindow.ShowDialog();
            if (result.GetValueOrDefault() == true)
            {
                //VM.SplitResults = splitsWindow.SplitResults;
                //VM.SaveSplitInsertResults();
            }
        }
   }
}
