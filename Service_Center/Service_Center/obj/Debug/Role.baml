   M S B A M L   `   `   `  ����I  EService_Center, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Service_CenterService_Center  5   	       Service_Center.Role   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Service_Center     5   	   -   5          � Title$  Роли��5   	   $	��600��6   $	��800��6#   
������� � WindowStartupLocation$ CenterScreen=�6@   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5       6!   �� 5!      6!   �� 5"      6!   �� 5#      6!   �� 5$      6!   �� 5%      6!   .+�5)   
   �� -   5        $��lbl_Supplier��5)      $��0��6$   $��5��61   $��3��6A   $��Роли��6U   $��Center=�6d   $��24(�6�   $��Center=�6�   $��Tahoma)�6�   �� 5*   
   $&�� D:\WPF_KP\Service_Center\ico.png��6   $��12��6:   &   System.Windows.Controls.DataGrid  5,   
   -   5        $��dgRole��5,         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6!      AutoGenerateColumns .    
IsReadOnly . $��1��6a   $��1��6q   $��11��6~   $��4��6�   6�   �� 5.   
   -   5        $��lbl_Name_of_Role��5.   $   $��Tahoma)�6   $��5��6<   $��1��6I   $��3��6Y   $ ��Название роли:��6m   $
��Left=�6�   $��14(�6�   $��Center=�6�   �� 5/   
   -   5        $��tb_Name_of_Role��5/   5   $��Tahoma)�6    �� 	MaxLength$ 30��6&   
�������$��5��6�   $��4��6�   $��2��6�   $��30��6�   ��6c   �� *SelectedItem.Name_of_Role+ �� ElementName$ dgRole��6�   �� 51   
   -   5        $��lbl_Access_of_Role��51   $   $��Tahoma)�6   $��6��6>   $��1��6K   $��3��6[   $)��#Выбор доступа роли:��6o   $
��Left=�6�   $��14(�6�   $��Center=�6�   �� 52   
   -   5        $��cb_Role��52      	 �� 
IsReadOnly	 . $��4��6~   $��6��6�   $��2��6�   $��30��6�   
 �� SelectedValue
 6"   �� *SelectedItem.Access_of_Role+$ dgRole��6�   �� 57   
   -   5        $��Search_1��57      $��Поиск:��6    $��8��61   $��16(�6>   $��Times New Roman)�6L   6k   �� 58      -	   5        $��	tb_Search��58      
�������$��8��6;   $��1��6H   $��2��6X   $��30��6l   $��Tahoma)�6x   �� 59   
   -
   5        $��bt_Insert_Role��59   8    � ToolTip$ Добавить��6   $��Tahoma)�6$   $��Добавить роль��6N   $��1��6g   $��9��6w   $��2��6�   $��30��6�   6�   �� 5:   
   -   5        $��bt_Update_Role��5:   8   $ Изменить��6   $��Tahoma)�6$   $��Изменить роль��6N   $��5��6g   $��9��6w   $��3��6�   $��30��6�   6�   �� 5;   
   -   5        $��bt_Delete_Role��5;   7   $ Удалить��6   $��Tahoma)�6#   $��Удалить роль��6M   $��10��6e   $��9��6v   $��2��6�   $��30��6�   6�   �� 5<   
   -   5        $��	bt_Search��5<   5   $ 
Поиск��6   $��Tahoma)�6!   $��
Поиск��6F   $��5��6W   $��8��6g   $��3��6t   $��30��6�   6�   �� 5=   
   -   5        $��	bt_Cancel��5=   8   $ Сбросить��6   $��Tahoma)�6$   $��Сбросить��6I   $��10��6]   $��8��6n   $��2��6{   $��30��6�   6�   �� 5?   
   -   5        $��bt_Back��5?   4   $3 -Вернуться в главное меню��6   $��Главное меню��6C   $��5��6[   $��10��6k   $��3��6y   $��30��6�   6�   5@      5A      