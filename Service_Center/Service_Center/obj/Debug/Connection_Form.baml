   M S B A M L   `   `   `  ����I  EService_Center, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Service_CenterService_Center  5   	   $    Service_Center.Connection_Form   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Service_Center     5   	   -   5          � Title$R  LНастройка подключения к источнику данных��5   	   $	��200��6:   $	��500��6G    � WindowStartupLocation$ CenterScreen=�6S   
������� � 
ResizeMode$ NoResize=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5!   
   �� -   5        $��lbl_Documents��5!   -   $��Times New Roman)�6   $��0��6B   $��1��6O   $��9��6_   $%��Название сервера��6s   $��Center=�6�   $��16(�6�   $��Center=�6�   �� 5"   
   -   5        $��
cb_Servers��5"       �� 
IsReadOnly . $��1��68   $��1��6H   $��9��6U   $��30��6i   �� 5$   
   -   5        $��lbl_bd��5$   -   $��Times New Roman)�6   $��2��6;   $��1��6H   $��9��6X   $��Выбор БД��6l   $��Center=�6   $��16(�6�   $��Center=�6�   �� 5%   
   -   5        $��cb_bd��5%       . $��1��63   $��3��6C   $��9��6P   $��30��6d   6r   �� 5'   
   -   5        $
��test��5'      $��5��61   $��1��6>   $��3��6N   $��30��6b   $��Проверка��6n   6�   �� 5(   
   -   5        $��test1��5(      $��Подключится��6   $��4��64   $��30��6A   $��7��6M   $��3��6]   $��Bottom=�6q   6�   5*      5+      