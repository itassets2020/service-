   M S B A M L   `   `   `  ����I  EService_Center, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Service_CenterService_Center  5   	   &     Service_Center.AuthorizationPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Service_Center     5   	     � Title$  Авторизация��5   	   $	��200��6   $	��500��6*   
������� � WindowStartupLocation$ CenterScreen=�6G   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5       6!   .+�5%   
   �� -   5        $ ��lbl_Авторизация��5%      $��0��6'   $��4��64   $��3��6D   $��Авторизация��6X   $��Center=�6n   $��14(�6�   $��Center=�6�   $��Tahoma)�6�   �� 5&   
   -   5        $��	lbl_Login��5&      $��1��6!   $��4��6.   $��3��6>   $��
Логин��6R   $��Center=�6b   $��16(�6�   $��Center=�6�   $��Tahoma)�6�   �� 5'   
   -   5        $��lbl_Password��5'      $��3��6$   $��4��61   $��3��6A   $��Пароль��6U   $��Center=�6f   $��16(�6�   $��Center=�6�   $��Tahoma)�6�   �� 5(   
   $&�� D:\WPF_KP\Service_Center\ico.png��6   $��10��6:   G� 5*   
   -   5        $��tbEnterLogin��5*      $��2��6*   $��Center=�67   $��11��6R   G� 5+   
   -   5        $��tbEnterPassword��5+      $��4��6-   $��Center=�6:   $��11��6U   �� 5-   
   -   5        $��bt_Login��5-   9    � ToolTip$ Вход в ИС��6   $��Tahoma)�6%   $��
Войти��6I   $��1��6Z   $��5��6j   $��4��6w   $��25��6�   $
��Left=�6�   $��0.333,0,0,0.667q�6�   $	��134��6�   �� 5.   
   -   5        $��bt_Back��5.   P   $C =Возвращение к окну атунтификации��6   $��Tahoma)�6<   $��
Назад��6_   $��7��6p   $��5��6�   $��3��6�   $��25��6�   6�   �� 5/   
   -   5        $��bt_Guest��5/   N   $= 7Нажмите чтобы войти, как гость��6   $��Tahoma)�6:   $��
Гость��6^   $��1��6o   $��0��6   $��4��6�   $��25��6�   $
��Left=�6�   $	��134��6�   6�   51      52      