A = 1; % Амплитуда
f = 1; % Частота
phi1 = 0; % Фаза для первой функции
phi2 = pi; % Фаза для второй функции (смещение на 180 градусов)

t = linspace(0, 2*pi, 100);

offset1 = 0.2; 
offset2 = 0.3; 
offset3 = 0.3; 

y1 = offset1 * (A * sin(2 * pi * f * t + phi1) + 0.5);
y2 = offset2 * (A * sin(2 * pi * f * t + phi2) + 0.5);

y3_smooth = smooth(rand(size(t)), 0.4, 'loess');
y3 = offset3 * (0.5 + y3_smooth - 0.5);

y_result = y1 + y2 + y3;

figure;
subplot(2,2,1);
h1 = plot(t(1), y1(1)*100, 'r'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_1 (1.0, 0.0)']);

subplot(2,2,2);
h2 = plot(t(1), y2(1)*100, 'g'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_2 (0.75, 0.25)']);

subplot(2,2,3);
h3 = plot(t(1), y3(1)*100, 'b'); 
xlabel('Время');
ylabel('Цена товара');
title(['Агент_3 (0.5, 0.5)']);

figure;
h_result = plot(t(1), y_result(1)*100/3, 'k'); 
xlabel('Время');
ylabel('Цена товара');
title('Результирующая цена товара');

for i = 2:length(t) 
    set(h1, 'XData', t(1:i), 'YData', y1(1:i)*100);
    set(h2, 'XData', t(1:i), 'YData', y2(1:i)*100);
    set(h3, 'XData', t(1:i), 'YData', y3(1:i)*100);
    set(h_result, 'XData', t(1:i), 'YData', y_result(1:i)*100/3);
    drawnow;
    pause(0.01);
end
