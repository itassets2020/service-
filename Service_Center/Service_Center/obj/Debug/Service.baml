   M S B A M L   `   `   `  ����I  EService_Center, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Service_CenterService_Center  5   	       Service_Center.Service   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Service_Center     5   	   -   5          � Title$  Услуги��5   	   $	��600��6   $	��800��6%   
������� � WindowStartupLocation$ CenterScreen=�6B   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5       6!   �� 5!      6!   �� 5"      6!   �� 5#      6!   �� 5$      6!   �� 5%      6!   .+�5(   
   �� -   5        $��Search_1��5(      $��Поиск:��6    $��8��61   $��16(�6>   $��Times New Roman)�6L   6k   �� 5*   
   -   5        $��lbl_Service��5*      $��0��6#   $��5��60   $��3��6@   $��Услуги��6T   $��Center=�6e   $��24(�6�   $��Center=�6�   $��Tahoma)�6�   �� 5+   
   $&�� D:\WPF_KP\Service_Center\ico.png��6   $��12��6:   �� 5-   
   -   5        $��lbl_Name_of_Service��5-   $   $��Tahoma)�6   $��5��6?   $��1��6L   $��3��6\   $$��Название услуги:��6p   $
��Left=�6�   $��14(�6�   $��Center=�6�   �� 5.   
   -   5        $��tb_Name_of_Service��5.   5    �� 	MaxLength$ 30��6   $��Tahoma)�6!   
�������$��5��6�   $��4��6�   $��3��6�   $��30��6�   ��6f   �� *SelectedItem.Name_of_Service+ �� ElementName$ 	dgService��6�   �� 50   
   -   5        $��lbl_Price_of_Service��50   $   $��Tahoma)�6   $��6��6@   $��1��6M   $��3��6]   $��Цена услуги:��6q   $
��Left=�6�   $��14(�6�   $��Center=�6�   �� 51   
   -   5        $��tb_Price_of_Service��51   5   $ 32��6   $��Tahoma)�6!   
�������$��6��6�   $��4��6�   $��3��6�   $��30��6�   ��6g   �� *SelectedItem.Price_of_Service+$ 	dgService��6�   �� 53   
   -   5        $��lbl_Select_Technic��53   .   $��Thimes New Roman)�6   $��7��6H   $��1��6U   $��3��6e   $ ��Выбор техники:��6y   $
��Left=�6�   $��14(�6�   $��Center=�6�   �� 54   
   -	   5        $��cb_Technic_Info��54       �� 
IsReadOnly . $��4��6�   $��7��6�   $��3��6�   $��30��6�    �� SelectedValue 6*   �� *SelectedItem.Technic_ID+$ 	dgService��6�   �� 58   
   -
   5        $��	tb_Search��58      
�������$��8��6:   $��1��6G   $��2��6W   $��30��6k   $��Tahoma)�6w   �� 59   
   -   5        $��bt_Insert_Service��59   9    � ToolTip$ Добавить��6   $��Tahoma)�6%   $#��Добавить услугу��6R   $��1��6m   $��9��6}   $��2��6�   $��30��6�   6�   �� 5:   
   -   5        $��bt_Update_Service��5:   8   $ Изменить��6   $��Tahoma)�6$   $#��Изменить услугу��6Q   $��5��6l   $��9��6|   $��3��6�   $��30��6�   6�   �� 5;   
   -   5        $��bt_Delete_Service��5;   7   $ Удалить��6   $��Tahoma)�6#   $!��Удалить услугу��6P   $��10��6j   $��9��6{   $��2��6�   $��30��6�   6�   �� 5<   
   -   5        $��	bt_Search��5<   5   $ 
Поиск��6   $��Tahoma)�6!   $��
Поиск��6F   $��5��6W   $��8��6g   $��3��6t   $��30��6�   6�   �� 5=   
   -   5        $��	bt_Cancel��5=   8   $ Сбросить��6   $��Tahoma)�6$   $��Сбросить��6I   $��10��6]   $��8��6n   $��2��6{   $��30��6�   6�   &   System.Windows.Controls.DataGrid  5?   
   -   5        $��	dgService��5?      	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
	 auto 6$   
   AutoGenerateColumns
 .    
IsReadOnly . $��1��6d   $��1��6t   $��11��6�   $��4��6�   6�   �� 5@   
   -   5        $��bt_Back��5@   4   $3 -Вернуться в главное меню��6   $��Главное меню��6C   $��5��6[   $��10��6k   $��3��6y   $��30��6�   6�   5B      5C      