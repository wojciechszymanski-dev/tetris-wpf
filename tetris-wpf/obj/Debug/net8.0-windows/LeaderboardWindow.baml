   M S B A M L   `   `   `  ����E  Atetris-wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:tetris_wpf
tetris_wpf  5   	   "    tetris_wpf.LeaderboardWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:tetris_wpf     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Leaderboard��5	   	   $	��450��6   $	��375��6*   .��5
      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��Top 10 Scores��6   $��24(�5      $
��Bold"�5      $��Center=�5      $��	0,20,0,20q�5      �� 5   
   -   5         ��Name$ scoresListView��5      $��1��5      $��10q�5       �� View 5      �� 5      .P�5      �� $
��Rank��6%    �� Width$ 40��63    �� DisplayMemberBinding 5   #   �� *Rank+�� 5      $
��Name��6%   $ 70��63    5   #   �� *
PlayerName+�� 5      $��Score��6%   $ 60��64    5    #   �� *Score+�� 5!      $��Level��6%   $ 60��64    5"   #   �� *
Difficulty+�� 5#      $
��Date��6%   $ 80��63    5$   #   �� *Date+ �� StringFormat$ {0:MM/dd HH:mm}��5%      5'      5(      5)      