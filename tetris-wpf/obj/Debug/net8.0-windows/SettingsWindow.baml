   M S B A M L   `   `   `  ����E  Atetris-wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:tetris_wpf
tetris_wpf  5   	       tetris_wpf.SettingsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:tetris_wpf     5   	     � Title$  SettingsWindow��5   	   $	��350��6    $	��330��6-    � 	Resources 5	         RadioButtonAsButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName��%"  &	        �� 5
   
   " 	�6,   .�5      ��  � �� 5      �� 5      " 	�6&   .R�5      �� $ border��6!   $��1q�5   !   
�������#��z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   -   5       5      P� 5       �@d�	IsChecked$	 FalseY�6;   . �5   "   �� $
 border��6)    � ��
 �����6S   5      5      5      �� 5       � ��$ 45��6'   �� 5       � ��
 �����6+   �� 5       � ��$
 Bold"�6+   �� 5        � ��$ 15(�6)   5!      5"      .��5#      �  � ColumnDefinitions 5$   
   �� 5%      $��30 �6   �� 5&      $��* �6   �� 5'      $��30 �6    � RowDefinitions 5)   
   �� 5*      $��10 �6   �� 5+      $��2* �6   �� 5,      $��* �6   �� 5-      $��8* �6   �� 5.      $��10 �6   .+�50   
   �� $��1��6   $��1��6    $��Tetris Settings��51      $��25(�52      $
��Bold"�6   $��Center=�53      $��Center=�54      �� 55   
   $��1��6   $��2��6    $��Difficulty level:��56      $��15(�57      $
��Bold"�6   �� 58   
   $��1��6   $��3��6%   .+�59      	� -   5        $
��Easy��59      #��[  5;      
��� � �$��0,0,0,10q�5=       	� 	GroupName$ 
Difficulty��5>       d� 	IsChecked$
 TrueY�5?      	� 5@      -   5        $��Medium��5@      #��[  5B      
��� ���$��0,0,0,10q�5D      $ 
Difficulty��5E      	� 5F      -   5        $
��Hard��5F      #��[  5H      
���  ��$ 
Difficulty��5J      5K      5L      5M      