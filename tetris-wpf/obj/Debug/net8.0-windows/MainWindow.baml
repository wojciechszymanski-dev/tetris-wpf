   M S B A M L   `   `   `  ����E  Atetris-wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:tetris_wpf
tetris_wpf  5   	       tetris_wpf.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:tetris_wpf     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
MainWindow��5	   	   $	��600��6   $	��330��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��10 �6   �� 5      $��* �6   �� 5      $��10 �6    � RowDefinitions 5   
   �� 5      $��10 �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��10 �6   .+�5   
   �� $��1��6   $��1��6    $$��/tetris-start-page-image-2.png��6-   $	��150��6U   $��0,50,0,0q�6b   �� 5   
   $��2��6   $��1��6"   $��Center=�62   $��0,0,0,50q�6M   .+�5      �� -   5         ��Name$ startButton��5      $
��Play��6+   $��18(�6:   $
��Bold"�6H   $	��200��6Z   $��40��6f   ���#eee���#333$��5q�6�   �� 5      -   5        $ settingsButton��5      $��Settings��6-   $��18(�6@   $
��Bold"�6N   $	��200��6`   $��40��6l   ���#eee���#333$��5q�6�   �� 5      -   5        $ LeaderboardButton��5      $��Leaderboard��60   $��18(�6F   $
��Bold"�6T   $	��200��6f   $��40��6r   ���#eee���#333$��5q�6�   5      5      5      