   M S B A M L   `   `   `  ����D  @WpfArasoi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfArasoi.ViewWpfArasoi.View  5   	   (    "WpfArasoi.View.AddChampionshipView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfArasoi.View     5   	     � Title$  	Adicionar��5   	   $	��600��6   $	��500��6(    � WindowStartupLocation$ CenterScreen=�64   .��5	      � -   5         �Name$ MainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Nome:��6   $��0��6    $��2��6-   �� 5   
   -   5         ��Name$ NameTextBox��5      $��1��6(   $��2��65   �� 5   
   $��Data de inicio��6   $��2��6)   $��0��66   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DateBeginDatePicker��5      $��3��62   $��0��6?   �� 5   
   $��Data de fim��6   $��2��6&   $��1��63     5    
   -   5        $ DateEndDatePicker��5       $��3��60   $��1��6=   �� 5#   
   -   5        $��	Confirmar��5#   7   $��4��6K   $��2��6X   5$      5%      